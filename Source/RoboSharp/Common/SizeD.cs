using System;
using System.ComponentModel;
using System.Globalization;

namespace RoboSharp
{
	/// <summary>
	/// Summary description for PointD.
	/// </summary>
	[TypeConverterAttribute(typeof(SizeDConverter))]
	public struct SizeD
	{
		private double m_dWidth;
		private double m_dHeight;

		[ReadOnlyAttribute(true)]
		public double Width
		{
			get {return m_dWidth;}
			set {m_dWidth = value;}
		}

		[ReadOnlyAttribute(true)]
		public double Height
		{
			get {return m_dHeight;}
			set {m_dHeight = value;}
		}

		public SizeD(double dWidth, double dHeight)
		{
			m_dWidth  = dWidth;
			m_dHeight = dHeight;
		}
	}

	public class SizeDConverter : ExpandableObjectConverter
	{
		public override bool CanConvertTo(ITypeDescriptorContext context,
			System.Type destinationType) 
		{
			if (destinationType == typeof(SizeD))
				return true;

			return base.CanConvertTo(context, destinationType);
		}

		public override object ConvertTo(ITypeDescriptorContext context,
			CultureInfo culture, 
			object value, 
			System.Type destinationType) 
		{
			if (destinationType == typeof(string) && 
				value is SizeD)
			{

				SizeD so = (SizeD)value;

				return Math.Round(so.Width) + ", " + Math.Round(so.Height);
			}
			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}