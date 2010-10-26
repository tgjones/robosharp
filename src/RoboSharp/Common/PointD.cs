using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for PointD.
	/// </summary>
	[TypeConverterAttribute(typeof(PointDConverter))]
	public struct PointD
	{
		private double m_dX;
		private double m_dY;

		public static PointD Empty = new PointD(0.0, 0.0);

		[ReadOnlyAttribute(true)]
		public double X
		{
			get {return m_dX;}
			set {m_dX = value;}
		}

		[ReadOnlyAttribute(true)]
		public double Y
		{
			get {return m_dY;}
			set {m_dY = value;}
		}

		public PointD(double dX, double dY)
		{
			m_dX = dX;
			m_dY = dY;
		}

		public static PointD operator + (PointD tA, PointD tB) 
		{
			return new PointD(tA.X + tB.X, tA.Y + tB.Y);
		}

		public static PointD operator - (PointD tA, PointD tB) 
		{
			return new PointD(tA.X - tB.X, tA.Y - tB.Y);
		}

		public static implicit operator PointF (PointD tPoint)
		{
			return new PointF((float) tPoint.X, (float) tPoint.Y);
		}
	}

	public class PointDConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context,
			System.Type destinationType) 
		{
			if (destinationType == typeof(PointD))
				return true;

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context,
			CultureInfo culture, 
			object value, 
			System.Type destinationType) 
		{
			if (destinationType == typeof(string) && 
				value is PointD)
			{

				PointD so = (PointD)value;

				return Math.Round(so.X) + ", " + Math.Round(so.Y);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
