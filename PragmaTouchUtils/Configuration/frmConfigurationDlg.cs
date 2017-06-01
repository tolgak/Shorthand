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

    private IConfigContentEditor _currentItem = null;
    private IList<IConfigContentEditor> _configItems = new List<IConfigContentEditor>();

    private ConfigAction _action = ConfigAction.Cancel;
    private ConfigContent _configContent = null;

    public IList<string> ChangedOptions
    {
      get { return _configItems.Where(x => x.Modified).Select(x => x.ItemClassName).ToList(); }
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

      //HostServicesSingleton.HostServices.SetMainFormAsOwner(this);      
		}




    private void CreateEditors()
    {
      var type = typeof(IConfigContentEditor);
      var editorTypes = AppDomain.CurrentDomain.GetAssemblies().ToList()
          .SelectMany(s => s.GetTypes())
          .Where(p => p.IsClass && type.IsAssignableFrom(p));

      foreach ( var editorType in editorTypes )
      {
        //ObjectHandle hnd = Activator.CreateInstance(editorType.Assembly.GetName().Name, editorType.FullName);
        //IConfigContentEditor editor = hnd.Unwrap() as IConfigContentEditor;
        var editor = Activator.CreateInstance(editorType) as IConfigContentEditor;

        if ( editor != null )
        {
          var matchedEditor = _configItems.FirstOrDefault(x => x.GetType() == editor.GetType());
          if ( matchedEditor != null )
          {
            _configItems.Remove(matchedEditor);
            ( matchedEditor as UserControl ).Dispose();
            matchedEditor = null;
          }
        }

        var editorAsControl = editor as UserControl;
        editorAsControl.Hide();
        editorAsControl.Parent = pnlContent;
        editorAsControl.Dock = DockStyle.Fill;

        _configItems.Add(editor);
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
      string key = string.Empty;
      TreeNode parentNode = AddNode( $"{ConfigContent.ApplicationName} Options");

      foreach ( var editor in _configItems )
      {
        TreeNode node = AddNode(parentNode, editor.Caption, editor.Caption);
        node.Tag = editor;
      }

      parentNode.Expand();
    }


		private TreeNode AddNode(TreeNode parent, string key, string text)
		{
			TreeNodeCollection nodes = (parent != null ? parent.Nodes : tv.Nodes);
			TreeNode result = nodes.Add(key, text);

			if (parent != null)
			{
				parent.ImageIndex = 1;
				parent.SelectedImageIndex = 1;
			}

      result.ImageIndex = 2;
      result.SelectedImageIndex = 0;
      
      return result;		
		}

    private TreeNode AddNode( string text )
    {
      return AddNode(null, text, text);
    }

    private TreeNode AddNode(TreeNode parent, string text )
    {
      return AddNode(parent, text, text);
    }
    
    private void RaiseFinalSelectionEvent( ConfigAction action )
    {
      //if (_onFinalSelection == null)
      //  return;

      var args = new ConfigEventArgs { action = action, content = _configContent, ChangedOptions = this.ChangedOptions };
      _onFinalSelection?.Invoke(this, args); ;							
    }

    private void ShowSelectedContent( TreeNode node )
    {
      if (node == null)      
        return;      

      if ( node.Tag == null || !(node.Tag is IConfigContentEditor))     
        return;      

      var configItem = node.Tag as IConfigContentEditor;
      if (_currentItem == configItem)      
        return;
      
      if (_currentItem != null)      
        _currentItem.HideContent();      

      _currentItem = configItem;
      if (!_currentItem.ContentLoaded)      
        _currentItem.LoadContent();
      
      lblHeader.Text = node.Text;
      configItem.ShowContent();
      lblHeader.SendToBack();
    }

    private bool SaveAllChangedContentItems()
    {
      bool result = false;
      foreach( IConfigContentEditor item in _configItems )
      {
        if(item.Modified)
        {
          item.SaveContent();
          result = true;
        }
      }

      return result;
    }

    private void SaveChanges()
    {
      if(SaveAllChangedContentItems())      
        _configContent.SaveConfiguration();
    }

    public void ShowOptionsEditor(string editorName)
    {
      if ( String.IsNullOrEmpty(editorName) )
        return;

      TreeNode[] nodes = tv.Nodes.Find(editorName, true);
      if ( nodes.Length > 0 )
        tv.SelectedNode = nodes[0];
    }

    public static void ShowConfigurationDlg(ConfigContent configContent, Form owner, ConfigFinalSelectionEventHandler onFinalSelectionHandler)
    {
      ShowConfigurationDlg(configContent, owner, string.Empty, onFinalSelectionHandler);
    }
    		
		public static void ShowConfigurationDlg(ConfigContent configContent, Form owner, string initialEditor, ConfigFinalSelectionEventHandler onFinalSelectionHandler)
		{
			if (configContent == null)			
				throw new ArgumentNullException("Configuration content is null!");			

			if (_instance == null)
				_instance = new frmConfigurationDlg();
      
      if ( onFinalSelectionHandler != null )
			  _instance.FinalSelection += onFinalSelectionHandler;

			_instance.InitializeConfiguration(configContent);
      _instance.StartPosition = FormStartPosition.Manual;
      _instance.Location = new Point(owner.Location.X + 100, owner.Location.Y + 100);      
      _instance.Show(owner);
      _instance.BringToFront();

      if ( !string.IsNullOrEmpty(initialEditor) )
        _instance.ShowOptionsEditor(initialEditor);
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
			_configItems.Add(editor);

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
      this.RaiseFinalSelectionEvent(_action);
			_instance = null;
		}

    private void btnApply_Click( object sender, EventArgs e )
    {
      _action = ConfigAction.Cancel;
      this.SaveChanges();
      this.RaiseFinalSelectionEvent(ConfigAction.Apply);
    }

    private void btnCancel_Click( object sender, EventArgs e )
    {
      _action = ConfigAction.Cancel;
      this.Close();
    }

    private void btnSave_Click( object sender, EventArgs e )
    {
      _action = ConfigAction.Save;
      this.SaveChanges();
      this.Close();
    }

    private void tv_AfterSelect( object sender, TreeViewEventArgs e )
    {
      ShowSelectedContent(e.Node);
    }

  }


  
} 