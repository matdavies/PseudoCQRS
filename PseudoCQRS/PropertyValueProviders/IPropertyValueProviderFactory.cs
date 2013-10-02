﻿using System.Collections.Generic;

namespace PseudoCQRS.PropertyValueProviders
{
	public interface IPropertyValueProviderFactory
	{
		IEnumerable<IPropertyValueProvider> GetPropertyValueProviders();
	}
}