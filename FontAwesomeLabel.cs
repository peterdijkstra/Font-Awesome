#pragma warning disable 0649 // UXMLReference variable declared but not assigned to.
using System;
using System.Collections.Generic;
using FontAwesome.Generated;
using UnityEngine;
//using UnityEditor;
using UnityEngine.UIElements;
//using UnityEditor.UIElements;

namespace Crossup.UIElements
{
	public class FontAwesomeLabel : Label
	{
		public new class UxmlFactory : UxmlFactory<FontAwesomeLabel, UxmlTraits>
		{
		}

		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			private readonly UxmlStringAttributeDescription brandIcon =
				new UxmlStringAttributeDescription
				{
					name = "brand", defaultValue = ""
				};

			private readonly UxmlStringAttributeDescription regularIcon =
				new UxmlStringAttributeDescription
				{
					name = "regular", defaultValue = ""
				};

			private readonly UxmlStringAttributeDescription solidIcon =
				new UxmlStringAttributeDescription
				{
					name = "solid", defaultValue = ""
				};

			private readonly UxmlStringAttributeDescription textAlign =
				new UxmlStringAttributeDescription
				{
					name = "text-anchor", defaultValue = "middle-center"
				};

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				var target = (FontAwesomeLabel)ve;
				base.Init(ve, bag, cc);

				var brandIconString = brandIcon.GetValueFromBag(bag, cc).Replace("-", "_");

				if (Enum.TryParse(brandIconString, true, out FontAwesomeBrands brand))
				{
					target.Brand = brand;
				}

				var regularIconString = regularIcon.GetValueFromBag(bag, cc).Replace("-", "_");

				if (Enum.TryParse(regularIconString, true, out FontAwesomeRegular regular))
				{
					target.Regular = regular;
				}

				var solidIconString = solidIcon.GetValueFromBag(bag, cc).Replace("-", "_");

				if (Enum.TryParse(solidIconString, true, out FontAwesomeSolid solid))
				{
					target.Solid = solid;
				}

				var textAlignString = textAlign.GetValueFromBag(bag, cc);

				if (Enum.TryParse(textAlignString, true, out TextAnchor textAnchor))
				{
					target.TextAnchor = textAnchor;
				}
			}
		}

		private FontAwesomeBrands brand = FontAwesomeBrands.NULL;

		private bool HasBrand => Brand != FontAwesomeBrands.NULL;

		public FontAwesomeBrands Brand
		{
			get => brand;
			set
			{
				brand = value;
				UpdateIcon();
			}
		}


		private FontAwesomeRegular regular = FontAwesomeRegular.NULL;

		private bool HasRegular => Regular != FontAwesomeRegular.NULL;

		public FontAwesomeRegular Regular
		{
			get => regular;
			set
			{
				regular = value;
				UpdateIcon();
			}
		}


		private FontAwesomeSolid solid = FontAwesomeSolid.NULL;

		private bool HasSolid => Solid != FontAwesomeSolid.NULL;

		public FontAwesomeSolid Solid
		{
			get => solid;
			set
			{
				solid = value;
				UpdateIcon();
			}
		}

		private TextAnchor textAnchor = TextAnchor.MiddleCenter;

		public TextAnchor TextAnchor
		{
			get => textAnchor;
			set
			{
				textAnchor = value;
				UpdateIcon();
			}
		}

		public FontAwesomeLabel() : base()
		{
		}

		public FontAwesomeLabel(FontAwesomeBrands icon, TextAnchor textAnchor = TextAnchor.MiddleCenter)
		{
			Brand = icon;
			TextAnchor = textAnchor;
			UpdateIcon();
		}

		public FontAwesomeLabel(FontAwesomeRegular icon, TextAnchor textAnchor = TextAnchor.MiddleCenter)
		{
			Regular = icon;
			TextAnchor = textAnchor;
			UpdateIcon();
		}

		public FontAwesomeLabel(FontAwesomeSolid icon, TextAnchor textAnchor = TextAnchor.MiddleCenter)
		{
			Solid = icon;
			TextAnchor = textAnchor;
			UpdateIcon();
		}

		private int IconAmount()
		{
			var counter = 0;
			if (HasBrand)
				counter++;
			if (HasRegular)
				counter++;
			if (HasSolid)
				counter++;

			return counter;
		}

		private void UpdateIcon()
		{
			if (IconAmount() == 0)
			{
				Debug.LogWarning("Make sure to set a FontAwesome icon");
				return;
			}

			if (IconAmount() > 1)
			{
				Debug.LogWarning("Make sure to set only 1 FontAwesome icon");
				return;
			}

			RemoveFromClassList("textElement");
			RemoveFromClassList("font-awesome-brand");
			RemoveFromClassList("font-awesome-regular");
			RemoveFromClassList("font-awesome-solid");

			var content = '\0';

			if (HasBrand)
			{
				AddToClassList("font-awesome-brands");
				content = (char)Brand;
			}

			if (HasRegular)
			{
				AddToClassList("font-awesome-regular");
				content = (char)Regular;
			}

			if (HasSolid)
			{
				AddToClassList("font-awesome-solid");
				content = (char)Solid;
			}

			style.unityTextAlign = TextAnchor;
			style.unityFontStyleAndWeight = FontStyle.Normal;


			text = content.ToString();
		}
	}
}