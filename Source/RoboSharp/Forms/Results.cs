using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for Results.
	/// </summary>
	public class Results : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button btnOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Results(Robot[] pWinnerRobots)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			int nLength = pWinnerRobots.Length;
			ArrayList pWinnersTemp = new ArrayList();
			Winner[] pWinners = new Winner[nLength];
			for (int i = 0; i < nLength; i++)
			{
				bool bContinue = false;
				for (int j = 0; j < pWinnersTemp.Count; j++)
				{
					if (((Winner) pWinnersTemp[j]).Name == pWinnerRobots[i].Name)
					{
						((Winner) pWinnersTemp[j]).Wins++;
						bContinue = true;
						continue;
					}
				}
				if (bContinue) continue;
				Winner pWinner = new Winner();
				pWinner.Name = pWinnerRobots[i].Name;
				pWinner.Wins = 1;
				pWinnersTemp.Add(pWinner);
			}

			nLength = pWinnersTemp.Count;
			for (int i = 0; i < nLength; i++)
			{
				ListViewItem pItem = new ListViewItem(((Winner) pWinnersTemp[i]).Name);
				pItem.SubItems.Add(((Winner) pWinnersTemp[i]).Wins.ToString());
				listView1.Items.Add(pItem);
			}
			listView1.Sort();
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
			this.btnOK = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(102, 138);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// listView1
			// 
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																																								this.columnHeader1,
																																								this.columnHeader2});
			this.listView1.GridLines = true;
			this.listView1.Location = new System.Drawing.Point(9, 9);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(263, 122);
			this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.listView1.TabIndex = 2;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Robot";
			this.columnHeader1.Width = 198;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Wins";
			// 
			// Results
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(281, 169);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Results";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Results";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private class Winner
		{
			public string Name;
			public int Wins;
		}
	}
}
