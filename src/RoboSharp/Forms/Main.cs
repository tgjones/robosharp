using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MainMenu menMain;
		private System.Windows.Forms.MenuItem menuItem9;
		private BattleGroundPanel pnlBattleGround;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.PropertyGrid pgrRobotDetails;
		private System.Windows.Forms.Button btnPause;
		
		private Battle m_pBattle;
		private System.Windows.Forms.ComboBox cboRobots;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private bool m_bPaused;

		public frmMain()
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
				if (components != null) 
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
			this.menMain = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.pnlBattleGround = new RoboSharp.BattleGroundPanel();
			this.pgrRobotDetails = new System.Windows.Forms.PropertyGrid();
			this.cboRobots = new System.Windows.Forms.ComboBox();
			this.btnPause = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// menMain
			// 
			this.menMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																						this.menuItem1,
																																						this.menuItem10,
																																						this.menuItem6});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItem2,
																																							this.menuItem4,
																																							this.menuItem3});
			this.menuItem1.Text = "Battle";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "New";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.Text = "-";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Exit";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 1;
			this.menuItem10.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							 this.menuItem11});
			this.menuItem10.Text = "Robot";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 0;
			this.menuItem11.Text = "Editor";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																																							this.menuItem9});
			this.menuItem6.Text = "Help";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 0;
			this.menuItem9.Text = "About";
			this.menuItem9.Click += new System.EventHandler(this.menuItem9_Click);
			// 
			// pnlBattleGround
			// 
			this.pnlBattleGround.BackColor = System.Drawing.Color.Black;
			this.pnlBattleGround.Location = new System.Drawing.Point(0, 0);
			this.pnlBattleGround.Name = "pnlBattleGround";
			this.pnlBattleGround.Size = new System.Drawing.Size(592, 544);
			this.pnlBattleGround.TabIndex = 1;
			// 
			// pgrRobotDetails
			// 
			this.pgrRobotDetails.CommandsVisibleIfAvailable = true;
			this.pgrRobotDetails.LargeButtons = false;
			this.pgrRobotDetails.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pgrRobotDetails.Location = new System.Drawing.Point(600, 56);
			this.pgrRobotDetails.Name = "pgrRobotDetails";
			this.pgrRobotDetails.Size = new System.Drawing.Size(216, 480);
			this.pgrRobotDetails.TabIndex = 2;
			this.pgrRobotDetails.Text = "pgrRobotDetails";
			this.pgrRobotDetails.ToolbarVisible = false;
			this.pgrRobotDetails.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pgrRobotDetails.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// cboRobots
			// 
			this.cboRobots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboRobots.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboRobots.Location = new System.Drawing.Point(600, 32);
			this.cboRobots.Name = "cboRobots";
			this.cboRobots.Size = new System.Drawing.Size(216, 21);
			this.cboRobots.TabIndex = 3;
			this.cboRobots.SelectedIndexChanged += new System.EventHandler(this.cboRobots_SelectedIndexChanged);
			// 
			// btnPause
			// 
			this.btnPause.Location = new System.Drawing.Point(600, 8);
			this.btnPause.Name = "btnPause";
			this.btnPause.Size = new System.Drawing.Size(216, 23);
			this.btnPause.TabIndex = 5;
			this.btnPause.Text = "Pause";
			this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(824, 544);
			this.Controls.Add(this.btnPause);
			this.Controls.Add(this.cboRobots);
			this.Controls.Add(this.pgrRobotDetails);
			this.Controls.Add(this.pnlBattleGround);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Menu = this.menMain;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "RoboSharp";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			using (frmMain pForm = new frmMain())
			{
				pForm.ShowSplashScreen();
				pForm.Show();
				
				while (pForm.Created)
				{
					pForm.ProcessFrame();
					Application.DoEvents();
				}
			}
		}

		public void CreateBattle(RobotListItem[] pRobots, int nNumRounds)
		{
			m_pBattle = new Battle(this, pnlBattleGround.ClientSize, pRobots, nNumRounds);

			for (int i = 0; i < m_pBattle.Robots.Length; i++)
			{
				cboRobots.Items.Add(m_pBattle.Robots[i].Name);
			}
			cboRobots.SelectedIndex = 0;
		}

		private void ShowSplashScreen()
		{
			SplashScreen pSplashScreen = new SplashScreen();
			pSplashScreen.Show();
			int nTick = Environment.TickCount;
			while (nTick + 1000 > Environment.TickCount)
			{

			}
			pSplashScreen.Hide();
		}

		private void ProcessFrame()
		{
			if (!m_bPaused && m_pBattle != null)
			{
				if (m_pBattle.ProcessFrame())
				{
					pnlBattleGround.Robots = m_pBattle.Robots;
					pnlBattleGround.Refresh();
					try
					{
						pgrRobotDetails.Refresh();
					}
					catch (InvalidOperationException) {}
				}
				else
				{
					Results pForm = new Results(m_pBattle.Winners);
					pForm.ShowDialog(this);
					m_pBattle = null;
				}
			}
		}

		private void Exit()
		{
			Environment.Exit(0);
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			Exit();
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Exit();
		}

		private void menuItem9_Click(object sender, System.EventArgs e)
		{
			AboutBox pForm = new AboutBox();
			pForm.ShowDialog(this);
		}

		private void btnPause_Click(object sender, System.EventArgs e)
		{
			if (m_bPaused)
			{
				m_bPaused = false;
				btnPause.Text = "Pause";
			}
			else
			{
				m_bPaused = true;
				btnPause.Text = "Resume";
			}
		}

		private void cboRobots_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			pgrRobotDetails.SelectedObject = m_pBattle.AllRobots[cboRobots.SelectedIndex];
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			Editor pForm = new Editor();
			pForm.Show();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			NewBattle pForm = new NewBattle(this);
			pForm.ShowDialog(this);
		}
	}
}