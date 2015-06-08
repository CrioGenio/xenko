﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using SharpYaml.Events;
using SiliconStudio.Core.IO;

namespace SiliconStudio.Core.Settings
{
    /// <summary>
    /// Arguments of the <see cref="SettingsKey.ChangesValidated"/> event.
    /// </summary>
    public class ChangesValidatedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangesValidatedEventArgs"/> class.
        /// </summary>
        /// <param name="profile">The profile in which changes have been validated.</param>
        public ChangesValidatedEventArgs(SettingsProfile profile)
        {
            Profile = profile;
        }

        /// <summary>
        /// Gets the <see cref="SettingsProfile"/> in which changes have been validated.
        /// </summary>
        public SettingsProfile Profile { get; private set; }

        /// <summary>
        /// Gets whether the profile in which changes have been validated is the current profile or not. 
        /// </summary>
        public bool IsCurrentProfile { get { return Profile == SettingsService.CurrentProfile; } }
    }

    /// <summary>
    /// This class represents property to store in the settings that is identified by a key.
    /// </summary>
    public abstract class SettingsKey
    {
        /// <summary>
        /// The default value of the settings key.
        /// </summary>
        protected readonly object DefaultObjectValue;

        /// <summary>
        /// The default value of the settings key.
        /// </summary>
        protected readonly Func<object> DefaultObjectValueCallback;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsKey"/> class.
        /// </summary>
        /// <param name="name">The name of this settings key. Must be unique amongst the application.</param>
        /// <param name="defaultValue">The default value associated to this settings key.</param>
        protected SettingsKey(UFile name, object defaultValue)
        {
            Name = name;
            DisplayName = name;
            DefaultObjectValue = defaultValue;
            IsEditable = true;
            SettingsService.RegisterSettingsKey(name, defaultValue, this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsKey"/> class.
        /// </summary>
        /// <param name="name">The name of this settings key. Must be unique amongst the application.</param>
        /// <param name="defaultValue">The default value associated to this settings key.</param>
        protected SettingsKey(UFile name, Func<object> defaultValueCallback)
        {
            Name = name;
            DisplayName = name;
            DefaultObjectValueCallback = defaultValueCallback;
            IsEditable = true;
            SettingsService.RegisterSettingsKey(name, defaultValueCallback(), this);
        }

        /// <summary>
        /// Gets the name of this <see cref="SettingsKey"/>.
        /// </summary>
        public UFile Name { get; private set; }

        /// <summary>
        /// Gets the type of this <see cref="SettingsKey"/>.
        /// </summary>
        public abstract Type Type { get; }

        /// <summary>
        /// Gets or sets whether this <see cref="SettingsKey"/> is editable by users.
        /// </summary>
        public bool IsEditable { get; set; }

        /// <summary>
        /// Gets or sets the display name of the <see cref="SettingsKey"/>.
        /// </summary>
        /// <remarks>The default value is the name parameter given to the constructor of this class.</remarks>
        public UFile DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description of this <see cref="SettingsKey"/>.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Raised when the value of the settings key has been modified and the method <see cref="SettingsProfile.ValidateSettingsChanges"/> has been invoked.
        /// </summary>
        public event EventHandler<ChangesValidatedEventArgs> ChangesValidated;

        /// <summary>
        /// Converts a value of a different type to the type associated with this <see cref="SettingsKey"/>. If the conversion is not possible,
        /// this method will return the default value of the SettingsKey.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted value if the conversion is possible, the default value otherwise.</returns>
        internal abstract object ConvertValue(List<ParsingEvent> value);

        /// <summary>
        /// Notifes that the changes have been validated by <see cref="SettingsProfile.ValidateSettingsChanges"/>.
        /// </summary>
        /// <param name="profile">The profile in which the change has been validated.</param>
        internal void NotifyChangesValidated(SettingsProfile profile)
        {
            var handler = ChangesValidated;
            if (handler != null)
                handler(this, new ChangesValidatedEventArgs(profile));
        }
    }
}