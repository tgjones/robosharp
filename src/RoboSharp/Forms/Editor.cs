using System;
using System.CodeDom.Compiler;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for Editor.
	/// </summary>
	public class Editor : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.RichTextBox txtEditor;
		private System.Windows.Forms.ListView lsvErrors;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label lblCompilationErrors;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItemCSharp;
		private System.Windows.Forms.MenuItem menuItemVB;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.MenuItem menuItem10;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string m_sFileName = null;

		public Editor()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.txtEditor = new System.Windows.Forms.RichTextBox();
			this.lsvErrors = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.lblCompilationErrors = new System.Windows.Forms.Label();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItemCSharp = new System.Windows.Forms.MenuItem();
			this.menuItemVB = new System.Windows.Forms.MenuItem();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItem1,
																																							this.menuItem2,
																																							this.menuItem9});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItem3,
																																							this.menuItem4,
																																							this.menuItem5,
																																							this.menuItem10,
																																							this.menuItem6,
																																							this.menuItem7});
			this.menuItem1.Text = "File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItem8});
			this.menuItem2.Text = "Compiler";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.menuItem3.Text = "New Robot";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.menuItem4.Text = "Open";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Enabled = false;
			this.menuItem5.Index = 2;
			this.menuItem5.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.menuItem5.Text = "Save";
			this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "-";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 5;
			this.menuItem7.Text = "Exit";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 0;
			this.menuItem8.Text = "Compile";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "C# files|*.cs|VB files|*.vb";
			// 
			// txtEditor
			// 
			this.txtEditor.AcceptsTab = true;
			this.txtEditor.AutoWordSelection = true;
			this.txtEditor.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtEditor.Location = new System.Drawing.Point(0, 0);
			this.txtEditor.Name = "txtEditor";
			this.txtEditor.Size = new System.Drawing.Size(632, 312);
			this.txtEditor.TabIndex = 1;
			this.txtEditor.Text = "";
			this.txtEditor.WordWrap = false;
			// 
			// lsvErrors
			// 
			this.lsvErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																																								this.columnHeader1,
																																								this.columnHeader2});
			this.lsvErrors.GridLines = true;
			this.lsvErrors.Location = new System.Drawing.Point(0, 344);
			this.lsvErrors.Name = "lsvErrors";
			this.lsvErrors.Size = new System.Drawing.Size(632, 104);
			this.lsvErrors.TabIndex = 2;
			this.lsvErrors.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Description";
			this.columnHeader1.Width = 568;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Line";
			// 
			// lblCompilationErrors
			// 
			this.lblCompilationErrors.Location = new System.Drawing.Point(6, 328);
			this.lblCompilationErrors.Name = "lblCompilationErrors";
			this.lblCompilationErrors.Size = new System.Drawing.Size(610, 16);
			this.lblCompilationErrors.TabIndex = 3;
			this.lblCompilationErrors.Text = "No Compilation Errors";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 2;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItemCSharp,
																																							this.menuItemVB});
			this.menuItem9.Text = "Language";
			// 
			// menuItemCSharp
			// 
			this.menuItemCSharp.Checked = true;
			this.menuItemCSharp.Index = 0;
			this.menuItemCSharp.RadioCheck = true;
			this.menuItemCSharp.Text = "C#";
			this.menuItemCSharp.Click += new System.EventHandler(this.menuItemCSharp_Click);
			// 
			// menuItemVB
			// 
			this.menuItemVB.Index = 1;
			this.menuItemVB.RadioCheck = true;
			this.menuItemVB.Text = "VB";
			this.menuItemVB.Click += new System.EventHandler(this.menuItemVB_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.Filter = "C# files|*.cs|VB files|*.vb";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 3;
			this.menuItem10.Text = "Save As";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// Editor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 446);
			this.Controls.Add(this.lblCompilationErrors);
			this.Controls.Add(this.lsvErrors);
			this.Controls.Add(this.txtEditor);
			this.Menu = this.mainMenu1;
			this.Name = "Editor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Robot Editor";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			txtEditor.Text = string.Empty;
			menuItem5.Enabled = false;
			m_sFileName = null;
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				txtEditor.LoadFile(openFileDialog1.FileName, RichTextBoxStreamType.PlainText);
				menuItem5.Enabled = true;
				m_sFileName = openFileDialog1.FileName;
			}
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			if (m_sFileName == null)
			{
				MessageBox.Show(this, "You must save this robot before trying to compile it.", string.Empty, MessageBoxButtons.OK);
				return;
			}

			// Compile script
			lsvErrors.Items.Clear();
			CodeDomProvider pProvider;
			if (menuItemCSharp.Checked)
			{
				pProvider = new Microsoft.CSharp.CSharpCodeProvider();
			}
			else
			{
				pProvider = new Microsoft.VisualBasic.VBCodeProvider();
			}
			CompilerResults results = CompileScript(
				txtEditor.Text,
				Application.ExecutablePath,
				Path.GetFileNameWithoutExtension(m_sFileName),
				pProvider);

			foreach (CompilerError err in results.Errors)
			{
				ListViewItem l;
				l = new ListViewItem(err.ErrorText);
				l.SubItems.Add(err.Line.ToString());
				lsvErrors.Items.Add(l);
			}

			if (results.Errors.Count == 0)
			{
				lblCompilationErrors.Text = "Compilation succeeded";
			}
			else
			{
				lblCompilationErrors.Text = "Compilation failed with " + results.Errors.Count + " errors";
			}
		}

		public static CompilerResults CompileScript(
			string sSource, string sReference, string sAssembly,
			CodeDomProvider pProvider)
		{
			ICodeCompiler pCompiler = pProvider.CreateCompiler();
			CompilerParameters pParams = new CompilerParameters();
			
			// configure parameters
			pParams.GenerateExecutable = false;
			pParams.GenerateInMemory = false;
			pParams.OutputAssembly = sAssembly + ".dll";
			pParams.IncludeDebugInformation = false;
			if (sReference != null && sReference.Length != 0)
			{
				pParams.ReferencedAssemblies.Add(sReference);
			}
			pParams.ReferencedAssemblies.Add("System.dll");
			pParams.ReferencedAssemblies.Add("System.Drawing.dll");
			
			// compile
			return pCompiler.CompileAssemblyFromSource(pParams, sSource);
		}

		private void menuItemVB_Click(object sender, System.EventArgs e)
		{
			menuItemVB.Checked = true;
			menuItemCSharp.Checked = false;
		}

		private void menuItemCSharp_Click(object sender, System.EventArgs e)
		{
			menuItemVB.Checked = false;
			menuItemCSharp.Checked = true;
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				m_sFileName = saveFileDialog1.FileName;
				menuItem5.Enabled = true;
				Save();
			}
		}

		private void Save()
		{
			txtEditor.SaveFile(m_sFileName, RichTextBoxStreamType.PlainText);
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			Save();
		}
	}
}
