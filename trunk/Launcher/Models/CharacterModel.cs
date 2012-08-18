﻿namespace Launcher.Models
{
    using Launcher.ViewModels;

    using System;
    using System.Collections.ObjectModel;
    using System.Xml.Linq;
    using System.ComponentModel;
    using System.IO;

    public class CharacterModel : INotifyPropertyChanged
    {
        #region Constructor(s)

        public CharacterModel(MasterViewModel viewModel)
        {
            m_ViewModel = viewModel;
            m_Characters = new ObservableCollection<string>();

        }

        #endregion

        #region Fields

        protected MasterViewModel m_ViewModel;

        #endregion

        #region Properties

        protected string m_BackupDirectory = String.Empty;
        /// <summary>
        ///     <para>Gets or sets a <see cref="System.String" /> value representing the directory where backups are to be placed.</para>
        /// </summary>
        public string BackupDirectory
        {
            get { return m_BackupDirectory; }
            set
            {
                if (m_BackupDirectory.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }

                m_BackupDirectory = value;
                OnPropertyChanged("BackupDirectory");
            }
        }

        protected string m_Current = String.Empty;
        /// <summary>
        ///     <para>Gets or sets a <see cref="System.String" /> value representing the currently selected character.</para>
        /// </summary>
        public string Current
        {
            get { return m_Current; }
            set
            {
                if (m_Current.Equals(value, StringComparison.InvariantCultureIgnoreCase))
                {
                    return;
                }

                m_Current = value;
                OnPropertyChanged("Character");
            }
        }

        protected ObservableCollection<string> m_Characters;
        /// <summary>
        ///     <para>Represents a collection of characters.</para>
        /// </summary>
        public ObservableCollection<string> Characters
        {
            get { return m_Characters; }
            protected set { m_Characters = value; }
        }

        #endregion

        #region Methods

        public bool Backup(string character)
        {
            throw new NotImplementedException();
        }

        public bool BackupAll()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     <para>Loads characters into the collection of characters.</para>
        /// </summary>
        public void Load(ref XDocument config)
        {
            if (!Directory.Exists(BackupDirectory))
            {
                Directory.CreateDirectory(BackupDirectory);
            }

            m_ViewModel.Log.Write("Loading characters");

            var characters = config.Element("Settings").Element("Characters");
            if (characters == null)
            {
                characters = new XElement("Characters", new XAttribute("LastSelected", String.Empty));
                config.Element("Settings").Add(characters);
            }

            if (characters.IsEmpty)
            {
                LoadCharactersFromDirectory();
            }
            else
            {
                LoadCharactersFromXml(ref config);
            }
        }

        protected void LoadCharactersFromDirectory()
        {
        }

        protected void LoadCharactersFromXml(ref XDocument x)
        {
        }

        public void Save(ref XDocument config)
        {
            // TODO: Save stuffz.
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
