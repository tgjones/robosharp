using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for NewBattle.
	/// </summary>
	public class NewBattle : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ListBox lsbGroups;
		private System.Windows.Forms.ListBox lsbRobots;
		private System.Windows.Forms.Label label3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private string[] m_pGroupNames;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox lsbSelectedRobots;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnRemoveAll;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btnStartBattle;
		private RobotListItem[] m_pRobotNames;
		private frmMain m_pParent;

		public NewBattle(frmMain pParentForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			m_pParent = pParentForm;

			LoadAvailableRobots();

			lsbGroups.SelectedIndex = 0;
		}

		private void LoadAvailableRobots()
		{
			ArrayList pGroupNames = new ArrayList();
			ArrayList pRobotNames = new ArrayList();
			ArrayList pDllNames = new ArrayList();

			lsbGroups.Items.Add("(All)");
			Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);
			string[] pFiles = Directory.GetFiles("robots");
			int nLength = pFiles.Length;
			for (int i = 0; i < nLength; i++)
			{
				try
				{
					if (Path.GetExtension(pFiles[i]).ToLower() == ".dll")
					{
						// attempt to load it and get the type name
						string sFileName = Path.GetFullPath(pFiles[i]);
						Assembly pAssembly = Assembly.LoadFile(sFileName);
						foreach (Type pType in pAssembly.GetTypes())
						{
							if (pType.BaseType.FullName == "RoboSharp.Robot")
							{
								string sNameSpace = pType.Namespace;
								string sFullType = pType.FullName;
								if (sNameSpace != null && !pGroupNames.Contains(sNameSpace))
								{
									pGroupNames.Add(sNameSpace);
									lsbGroups.Items.Add(sNameSpace);
								}
								pRobotNames.Add(sFullType);
								pDllNames.Add(sFileName);
							}
						}
					}
				}
				catch
				{

				}
			}
			lsbGroups.Items.Add("(No group)");

			nLength = pRobotNames.Count;
			m_pRobotNames = new RobotListItem[nLength];
			for (int i = 0; i < nLength; i++)
			{
				m_pRobotNames[i].Name = pRobotNames[i].ToString();
				m_pRobotNames[i].Dll = pDllNames[i].ToString();
			}

			m_pGroupNames = new string[pGroupNames.Count];
			pGroupNames.CopyTo(m_pGroupNames);
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
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lsbRobots = new System.Windows.Forms.ListBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lsbGroups = new System.Windows.Forms.ListBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lsbSelectedRobots = new System.Windows.Forms.ListBox();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnRemoveAll = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.btnStartBattle = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select robots for the battle";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lsbRobots);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.lsbGroups);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Location = new System.Drawing.Point(8, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 224);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Available Robots";
			// 
			// lsbRobots
			// 
			this.lsbRobots.Location = new System.Drawing.Point(136, 40);
			this.lsbRobots.Name = "lsbRobots";
			this.lsbRobots.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lsbRobots.Size = new System.Drawing.Size(120, 173);
			this.lsbRobots.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(136, 24);
			this.label3.Name = "label3";
			this.label3.TabIndex = 3;
			this.label3.Text = "Robots";
			// 
			// lsbGroups
			// 
			this.lsbGroups.Location = new System.Drawing.Point(8, 40);
			this.lsbGroups.Name = "lsbGroups";
			this.lsbGroups.Size = new System.Drawing.Size(120, 173);
			this.lsbGroups.TabIndex = 0;
			this.lsbGroups.SelectedIndexChanged += new System.EventHandler(this.lsbGroups_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "Groups";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lsbSelectedRobots);
			this.groupBox2.Location = new System.Drawing.Point(376, 32);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(136, 224);
			this.groupBox2.TabIndex = 4;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Selected Robots";
			// 
			// lsbSelectedRobots
			// 
			this.lsbSelectedRobots.Location = new System.Drawing.Point(8, 27);
			this.lsbSelectedRobots.Name = "lsbSelectedRobots";
			this.lsbSelectedRobots.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.lsbSelectedRobots.Size = new System.Drawing.Size(120, 186);
			this.lsbSelectedRobots.TabIndex = 0;
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(280, 56);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(88, 23);
			this.btnAdd.TabIndex = 5;
			this.btnAdd.Text = "Add ->";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnRemoveAll
			// 
			this.btnRemoveAll.Location = new System.Drawing.Point(280, 224);
			this.btnRemoveAll.Name = "btnRemoveAll";
			this.btnRemoveAll.Size = new System.Drawing.Size(88, 23);
			this.btnRemoveAll.TabIndex = 6;
			this.btnRemoveAll.Text = "<- Remove All";
			this.btnRemoveAll.Click += new System.EventHandler(this.btnRemoveAll_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(280, 192);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(88, 23);
			this.btnRemove.TabIndex = 7;
			this.btnRemove.Text = "<- Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(280, 88);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(88, 23);
			this.button1.TabIndex = 8;
			this.button1.Text = "Add All ->";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(264, 272);
			this.button2.Name = "button2";
			this.button2.TabIndex = 9;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// btnStartBattle
			// 
			this.btnStartBattle.Enabled = false;
			this.btnStartBattle.Location = new System.Drawing.Point(184, 272);
			this.btnStartBattle.Name = "btnStartBattle";
			this.btnStartBattle.TabIndex = 10;
			this.btnStartBattle.Text = "Start Battle";
			this.btnStartBattle.Click += new System.EventHandler(this.btnStartBattle_Click);
			// 
			// NewBattle
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(522, 304);
			this.Controls.Add(this.btnStartBattle);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnRemoveAll);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "NewBattle";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Battle";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NewBattle_KeyDown);
			this.Load += new System.EventHandler(this.NewBattle_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void lsbGroups_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string sSelected = ((ListBox) sender).SelectedItem.ToString();
			lsbRobots.Items.Clear();
			int nLength = m_pRobotNames.Length;
			if (sSelected == "(All)")
			{
				AddRange(lsbRobots, m_pRobotNames);
			}
			else if (sSelected == "(No group)")
			{
				for (int i = 0; i < nLength; i++)
				{
					if (m_pRobotNames[i].ToString().IndexOf(".") == -1)
					{
						lsbRobots.Items.Add(m_pRobotNames[i]);
					}
				}
			}
			else
			{
				if (m_pGroupNames.Length > 0)
				{
					string sNameSpace = m_pGroupNames[lsbGroups.SelectedIndex - 1].ToString();
					for (int i = 0; i < nLength; i++)
					{
						if (m_pRobotNames[i].ToString().StartsWith(sNameSpace + "."))
						{
							lsbRobots.Items.Add(m_pRobotNames[i]);
						}
					}
				}
			}
		}

		private void NewBattle_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F5)
			{
				LoadAvailableRobots();
				lsbGroups.SelectedIndex = 0;
			}
		}

		private void NewBattle_Load(object sender, System.EventArgs e)
		{
		
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void btnRemoveAll_Click(object sender, System.EventArgs e)
		{
			lsbSelectedRobots.Items.Clear();
			btnStartBattle.Enabled = false;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			AddRange(lsbSelectedRobots, m_pRobotNames);
			if (lsbSelectedRobots.Items.Count > 1) btnStartBattle.Enabled = true;
		}

		private void AddRange(ListBox pListBox, RobotListItem[] pItems)
		{
			int nLength = pItems.Length;
			for (int i = 0; i < nLength; i++)
			{
				pListBox.Items.Add(pItems[i]);
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			if (lsbRobots.SelectedItems != null)
			{
				for (int i = 0; i < lsbRobots.SelectedItems.Count; i++)
				{
					lsbSelectedRobots.Items.Add(lsbRobots.SelectedItems[i]);
				}
			}
			lsbRobots.SelectedIndex = -1;
			if (lsbSelectedRobots.Items.Count > 1) btnStartBattle.Enabled = true;
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if (lsbSelectedRobots.SelectedItems != null)
			{
				for (int i = 0; i < lsbSelectedRobots.SelectedItems.Count; i++)
				{
					lsbSelectedRobots.Items.Remove(lsbSelectedRobots.SelectedItems[i]);
				}
			}
			lsbSelectedRobots.SelectedIndex = -1;
			if (lsbSelectedRobots.Items.Count < 2) btnStartBattle.Enabled = false;
		}

		private void btnStartBattle_Click(object sender, System.EventArgs e)
		{
			int nLength = lsbSelectedRobots.Items.Count;
			RobotListItem[] pRobots = new RobotListItem[nLength];
			for (int i = 0; i < nLength; i++)
			{
				pRobots[i] = (RobotListItem) lsbSelectedRobots.Items[i];
			}
			m_pParent.CreateBattle(pRobots, 3);
			Close();
		}
	}

	public struct RobotListItem
	{
		public string Name;
		public string Dll;

		public override string ToString()
		{
			return Name;
		}
	}
}
