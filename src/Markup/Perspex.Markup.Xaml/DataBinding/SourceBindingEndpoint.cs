// Copyright (c) The Perspex Project. All rights reserved.
// Licensed under the MIT license. See licence.md file in the project root for full license information.

using System;
using System.ComponentModel;

namespace Perspex.Markup.Xaml.DataBinding
{
    public class SourceBindingEndpoint
    {
        public Type PropertyType { get; }

        public INotifyPropertyChanged Source { get; }

        public dynamic PropertyGetter { get; }

        public Delegate PropertySetter { get; }

        public SourceBindingEndpoint(INotifyPropertyChanged source, Type propertyType, dynamic propertyGetter, Delegate propertySetter)
        {
            this.Source = source;
            this.PropertyType = propertyType;
            this.PropertyGetter = propertyGetter;
            this.PropertySetter = propertySetter;
        }
    }
}