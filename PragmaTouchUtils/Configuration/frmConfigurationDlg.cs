/********************************************************************
  Class      : frmConfiguration
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace PragmaTouchUtils
{
  public enum ConfigAction
  {
    None,
    Cancel,
    Save,
    Apply
  }

  public partial class frmConfigurationDlg : Form
	{
		private static frmConfigurationDlg _instance = null;

    public static void Show(ConfigContent configContent, Form owner, ConfigFinalSelectionEventHandler onFinalSelectionHandler, string initialEditor = "")
    {
      if (configContent == null)
        throw new ArgumentNullException("Configuration content is null!");

      if (_instance == null)
        _instance = new frmConfigurationDlg();

      if (onFinalSelectionHandler != null)
        _instance.FinalSelection += onFinalSelectionHandler;

      _instance.InitializeConfiguration(configContent);
      _instance.StartPosition = FormStartPosition.Manual;
      _instance.Location = new Point(owner.Location.X + 100, owner.Location.Y + 100);
      _instance.Show(owner);
      _instance.BringToFront();

      if (!string.IsNullOrEmpty(initialEditor))
        _instance.ShowOptionsEditor(initialEditor);
    }


    private IConfigContentEditor _currentItem = null;
    private List<IConfigContentEditor> _editors = new List<IConfigContentEditor>();

    private ConfigContent _configContent = null;

    public List<string> ChangedOptions
    {
      get { return _editors.Where(x => x.Modified).Select(x => x.ItemClassName).ToList(); }
    }

    private ConfigFinalSelectionEventHandler _onFinalSelection;
    public event ConfigFinalSelectionEventHandler FinalSelection
    {
      add
      {
        _onFinalSelection += value;
      }
      remove
      {
        _onFinalSelection -= value;
      }
    }
		
		public TreeNode ModuleOptionsRoot { get; set; }
		
		public frmConfigurationDlg( )
    {
      InitializeComponent();
      this.CreateEditors();
		}

    public void ShowOptionsEditor(string editorName)
    {
      if (string.IsNullOrEmpty(editorName))
        return;

      var node = tv.Nodes.Find(editorName, true).FirstOrDefault();
      if (node != null)
        tv.SelectedNode = node;
    }

    private void ShowSelectedContent(TreeNode node)
    {
      if ( !(node?.Tag is IConfigContentEditor configItem) )
        return;

      if (_currentItem == configItem)
        return;

      _currentItem?.HideContent();
      _currentItem = configItem;
      if (!_currentItem.ContentLoaded)
        _currentItem.LoadContent();

      lblHeader.Text = node.Text;
      configItem.ShowContent();
      lblHeader.SendToBack();
    }

    private void CreateEditors()
    {
      var type = typeof(IConfigContentEditor);
      var editorTypes = AppDomain.CurrentDomain.GetAssemblies()
                                 .SelectMany(s => s.GetTypes())
                                 .Where(p => p.IsClass && type.IsAssignableFrom(p));

      foreach ( var editorType in editorTypes )
      {
        var editor = Activator.CreateInstance(editorType) as IConfigContentEditor;
        var matchedEditor = _editors.FirstOrDefault(x => x.GetType() == editorType);
        if ( matchedEditor != null )
        {
          _editors.Remove(matchedEditor);
          ( matchedEditor as UserControl ).Dispose();
          matchedEditor = null;
        }
        _editors.Add(editor);
        
        var editorAsControl = editor as UserControl;
        editorAsControl.Hide();
        editorAsControl.Parent = pnlContent;
        editorAsControl.Dock = DockStyle.Fill;
      }      
    }

    public void InitializeConfiguration(ConfigContent configContent)
    {
      _configContent = configContent ?? throw new ArgumentNullException("Configuration content is null!");
      this.Text = $"{ConfigContent.ApplicationName} options";

      BuildConfigurationItems();
    }

    private void BuildConfigurationItems( )
    {
      tv.Nodes.Clear();
      var parentNode = this.AddNode( $"{ConfigContent.ApplicationName} Options");

      _editors.ForEach(x => this.AddNode(parentNode, x.Caption).Tag = x);
      parentNode.Expand();
    }

    private TreeNode AddNode( string text )
    {
      return AddNode(null, text, text);
    }

    private TreeNode AddNode(TreeNode parent, string text )
    {
      return AddNode(parent, text, text);
    }
    
		private TreeNode AddNode(TreeNode parent, string text, string key)
		{
			var nodes  = parent != null ? parent.Nodes : tv.Nodes;
			var result = nodes.Add(key, text, 2, 0);

      if (parent != null)
      {
        parent.ImageIndex = 1;
				parent.SelectedImageIndex = 1;
      }
      
      return result;		
		}




    private void SaveChanges()
    {
      _editors.Where(x => x.Modified).ToList().ForEach(item => item.SaveContent());
    }

 		#region IConfigSvc Support Methods

		public TreeNode AddFolder(string text)
		{
			TreeNode node = this.ModuleOptionsRoot.Nodes.Add(text);
			node.Text = text;
			node.ImageIndex = 4;
			node.SelectedImageIndex = 4;
			return node;
		}

		public TreeNode AddItem(TreeNode parent, string text, IConfigContentEditor editor)
		{
			if (String.IsNullOrEmpty(text))
				throw new ArgumentNullException("Can not add item to configuration dialog. Text is null or empty.");

			if (editor == null )
				return null;

			UserControl uc = editor as UserControl;
			if (uc == null)
				return null;

			TreeNode node = null;
			if (parent == null)
				node = this.ModuleOptionsRoot.Nodes.Add(text,text);
			else
				node = parent.Nodes.Add(text,text);

			node.Text = text;
			node.ImageIndex = 2;
			node.SelectedImageIndex = 0;
			node.Tag = editor;
      _editors.Add(editor);

			uc.Hide();
			uc.Parent = pnlContent;
			uc.Dock = DockStyle.Fill;

			return node;
		}

		#endregion 


    private void frmConfigurationDlg_FormClosed( object sender, FormClosedEventArgs e )
    {
      _instance.Hide();
      Application.DoEvents();

      _instance = null;
		}

    private void btnApply_Click( object sender, EventArgs e )
    {
      var changedOptions = this.ChangedOptions;
      
      this.SaveChanges();
      var args = new ConfigEventArgs { action = ConfigAction.Apply, content = _configContent, ChangedOptions = changedOptions };
      _onFinalSelection?.Invoke(this, args);
    }

    private void btnCancel_Click( object sender, EventArgs e )
    {
      var args = new ConfigEventArgs { action = ConfigAction.Cancel, content = _configContent, ChangedOptions = this.ChangedOptions };
      _onFinalSelection?.Invoke(this, args);

      this.Close();
    }

    private void btnSave_Click( object sender, EventArgs e )
    {
      var changedOptions = this.ChangedOptions;
      
      this.SaveChanges();
      var args = new ConfigEventArgs { action = ConfigAction.Save, content = _configContent, ChangedOptions = changedOptions };
      _onFinalSelection?.Invoke(this, args);

      this.Close();
    }

    private void tv_AfterSelect( object sender, TreeViewEventArgs e )
    {
      this.ShowSelectedContent(e.Node);
    }

  }


  
} 