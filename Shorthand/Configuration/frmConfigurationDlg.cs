/********************************************************************
  Class      : frmConfiguration
  Created by : Ali Özgür
  Contact    : ali_ozgur@hotmail.com

  Copyright  : Ali Özgür - 2007
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Runtime.Remoting;

using PragmaTouchUtils;


namespace Shorthand
{
  //public enum ConfigAction
  //{
  //  None,
  //  Cancel,
  //  Save,
  //  Apply
  //}

  public partial class frmConfigurationDlg : Form
  {
    private static frmConfigurationDlg _instance = null;
    public static frmConfigurationDlg Instance
    {
      get { return _instance; }
    }

    private IConfigContentEditor _currentItem = null;
    private IList<IConfigContentEditor> _configItems = new List<IConfigContentEditor>();

    private ConfigAction _action = ConfigAction.Cancel;
    private ConfigContent _configContent = null;

    //private IList<string> _changedOptions = new List<string>();
    //public IList<string> ChangedOptions
    //{
    //  get { return _configItems.Where( x => x.Modified).Select( x => x.ItemClassName).ToList(); }
    //}

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

    private TreeNode _moduleOptionsRoot = null;
    public TreeNode ModuleOptionsRoot
    {
      get { return _moduleOptionsRoot; }
      set { _moduleOptionsRoot = value; }
    }

    public frmConfigurationDlg()
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
        ObjectHandle hnd = Activator.CreateInstance(editorType.Assembly.GetName().Name, editorType.FullName);
        IConfigContentEditor editor = hnd.Unwrap() as IConfigContentEditor;

        if ( editor != null )
        {
          IConfigContentEditor matchedEditor = _configItems.FirstOrDefault(x => x.GetType() == editor.GetType());
          if ( matchedEditor != null )
          {
            _configItems.Remove(matchedEditor);
            ( matchedEditor as UserControl ).Dispose();
            matchedEditor = null;
          }
        }

        UserControl editorAsControl = editor as UserControl;
        editorAsControl.Hide();
        editorAsControl.Parent = pnlContent;
        editorAsControl.Dock = DockStyle.Fill;

        _configItems.Add(editor);
      }

    }

    public void InitializeConfiguration(ConfigContent configContent)
    {
      if ( configContent == null )
        throw new ArgumentNullException("Configuration content is null!");

      _configContent = configContent;
      this.Text = string.Format("{0} options", ConfigContent.ApplicationName);

      BuildConfigurationItems();
    }

    private void BuildConfigurationItems()
    {
      tv.Nodes.Clear();
      string key = string.Empty;
      TreeNode parentNode = AddNode(string.Format("{0} Options", ConfigContent.ApplicationName));

      foreach ( var editor in _configItems )
      {
        TreeNode node = AddNode(parentNode, editor.Caption, editor.Caption);
        node.Tag = editor;
      }

      parentNode.Expand();

      //      _moduleOptionsRoot = tv.Nodes.Add("Modules");
      //      _moduleOptionsRoot.ImageIndex = 1;
      //      _moduleOptionsRoot.SelectedImageIndex = 1;
    }


    private TreeNode AddNode(TreeNode parent, string key, string text)
    {
      TreeNodeCollection nodes = ( parent != null ? parent.Nodes : tv.Nodes );
      TreeNode result = nodes.Add(key, text);

      if ( parent != null )
      {
        parent.ImageIndex = 1;
        parent.SelectedImageIndex = 1;
      }

      result.ImageIndex = 2;
      result.SelectedImageIndex = 0;

      return result;
    }

    private TreeNode AddNode(string text)
    {
      return AddNode(null, text, text);
    }

    private TreeNode AddNode(TreeNode parent, string text)
    {
      return AddNode(parent, text, text);
    }

    private void RaiseFinalSelectionEvent(ConfigAction action)
    {
      if ( _onFinalSelection != null )
      {
        ConfigEventArgs args = new ConfigEventArgs();
        args.action = action;
        args.content = _configContent;
        args.ChangedOptions = _configItems.Where(x => x.Modified).Select(x => x.ItemClassName).ToList();

        _onFinalSelection(this, args);
      }
    }

    private void ShowSelectedContent(TreeNode node)
    {
      if ( node == null )
        return;

      if ( node.Tag == null || !( node.Tag is IConfigContentEditor ) )
        return;

      IConfigContentEditor configItem = node.Tag as IConfigContentEditor;
      if ( _currentItem == configItem )
        return;

      if ( _currentItem != null )
        _currentItem.HideContent();

      _currentItem = configItem;
      if ( !_currentItem.ContentLoaded )
        _currentItem.LoadContent();



      header.Text = node.Text;


      configItem.ShowContent();


    }

    private bool SaveAllChangedContentItems()
    {
      bool result = false;
      foreach ( IConfigContentEditor item in _configItems )
      {
        if ( item.Modified )
        {
          item.SaveContent();
          result = true;
        }
      }

      return result;
    }

    private void SaveChanges()
    {
      if ( SaveAllChangedContentItems() )
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
      if ( configContent == null )
        throw new ArgumentNullException("Configuration content is null!");

      if ( _instance == null )
        _instance = new frmConfigurationDlg();

      if ( onFinalSelectionHandler != null )
        _instance.FinalSelection += onFinalSelectionHandler;

      _instance.InitializeConfiguration(configContent);
      //ConficSvc.FireDialogOpenedEvent();


      _instance.StartPosition = FormStartPosition.CenterParent;
      _instance.ShowDialog();


      if ( !string.IsNullOrEmpty(initialEditor) )
        _instance.ShowOptionsEditor(initialEditor);
    }



    #region IConfigSvc Support Methods

    public TreeNode AddFolder(string text)
    {
      TreeNode node = _moduleOptionsRoot.Nodes.Add(text);
      node.Text = text;
      node.ImageIndex = 4;
      node.SelectedImageIndex = 4;
      return node;
    }

    public TreeNode AddItem(TreeNode parent, string text, IConfigContentEditor editor)
    {
      if ( String.IsNullOrEmpty(text) )
        throw new ArgumentNullException("Can not add item to configuration dialog. Text is null or empty.");

      if ( editor == null )
        return null;

      UserControl uc = editor as UserControl;
      if ( uc == null )
        return null;

      TreeNode node = null;
      if ( parent == null )
        node = _moduleOptionsRoot.Nodes.Add(text, text);
      else
        node = parent.Nodes.Add(text, text);

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

    #endregion //IConfigSvc Support Methods




    private void frmConfigurationDlg_FormClosed(object sender, FormClosedEventArgs e)
    {
      _instance.Hide();
      Application.DoEvents();
      this.RaiseFinalSelectionEvent(_action);
      //frmConfigurationDlg.ConficSvc.FireDialogClosedEvent();
      _instance = null;
    }

    private void btnApply_Click(object sender, EventArgs e)
    {
      _action = ConfigAction.Cancel;
      this.SaveChanges();
      this.RaiseFinalSelectionEvent(ConfigAction.Apply);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      _action = ConfigAction.Cancel;
      this.Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      _action = ConfigAction.Save;
      this.SaveChanges();
      this.Close();
    }

    private void tv_AfterSelect(object sender, TreeViewEventArgs e)
    {
      ShowSelectedContent(e.Node);
    }

  }



}