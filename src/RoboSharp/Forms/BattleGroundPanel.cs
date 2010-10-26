using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for BattleGroundPanel.
	/// </summary>
	public class BattleGroundPanel : Panel
	{
		private Robot[] m_pObjects;
		private Font m_pLabelFont;

		public Robot[] Robots
		{
			set {m_pObjects = value;}
		}

		public BattleGroundPanel()
		{
			// set styles for double buffering (reduces flicker)
			SetStyle(ControlStyles.DoubleBuffer, true);
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			m_pLabelFont = new Font("Arial", 7);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (m_pObjects != null)
			{
				int nLength = m_pObjects.Length;
				for (int i = 0; i < nLength; i++)
				{
					if (m_pObjects[i].IsAlive)
					{
						PointD tPositionD = m_pObjects[i].Position;
						Point tPosition = new Point((int) tPositionD.X, (int) tPositionD.Y);
						tPosition.Y = Height - tPosition.Y;
						SizeD tSizeD = m_pObjects[i].Size;

						// position labels
						PointF tLabelEnergy = new PointF(tPosition.X, tPosition.Y - 35);
						PointF tLabelName = new PointF(tPosition.X, tPosition.Y + 27);
						if (tLabelEnergy.Y < 10) tLabelEnergy.Y = 10;
						if (tLabelName.Y > Height - 20) tLabelName.Y = Height - 20;

						// draw bullets
						e.Graphics.ResetTransform();
						for (int j = 0; j < m_pObjects[i].Bullets.Length; j++)
						{
							Bullet pBullet = m_pObjects[i].Bullets[j];
							e.Graphics.FillRectangle(new SolidBrush(Color.White),
								(float) pBullet.Position.X, Height - (float) pBullet.Position.Y,
								5, 5);
						}

						// draw robot
						e.Graphics.ResetTransform();
						tPosition.X -= (int) (tSizeD.Width  / 2.0);
						tPosition.Y -= (int) (tSizeD.Height / 2.0);
						Matrix tMatrix = new Matrix();
						tMatrix.Translate((float) tPosition.X, (float) tPosition.Y);
						PointF tRotationPoint = new PointF((float) (tSizeD.Width / 2.0f),
							(float) (tSizeD.Height / 2.0f));
						tMatrix.RotateAt((float) m_pObjects[i].Heading, tRotationPoint);
						e.Graphics.Transform = tMatrix;
						e.Graphics.DrawImage(m_pObjects[i].BaseBitmap, 0, 0, 50, 50);

						tMatrix.Translate(0, 0);
						e.Graphics.Transform = tMatrix;
						e.Graphics.DrawImage(m_pObjects[i].GunBitmap, 0, 0, 50, 50);

						// draw labels
						e.Graphics.ResetTransform();
						StringFormat pFormat = new StringFormat();
						pFormat.Alignment = StringAlignment.Center;
						e.Graphics.DrawString(m_pObjects[i].Energy.ToString(), m_pLabelFont,
							new SolidBrush(Color.White), tLabelEnergy, pFormat);
						e.Graphics.DrawString(m_pObjects[i].Name, m_pLabelFont,
							new SolidBrush(Color.White), tLabelName, pFormat);

						/*tPosition = new Point((int) tPositionD.X, (int) tPositionD.Y);
						tPosition.Y = Height - tPosition.Y;
						PointF tEndPoint = m_pObjects[i].EndPoint;
						tEndPoint.Y = Height - tEndPoint.Y;
						Color tColour = (i == 0) ? Color.Red : Color.Blue;
						Pen pPen = new Pen(tColour);
						e.Graphics.DrawLine(pPen, tPosition, tEndPoint);*/
					}
				}
			}
		}
	}
}