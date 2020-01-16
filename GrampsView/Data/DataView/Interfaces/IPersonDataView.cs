//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="IPersonDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections.ObjectModel;

    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;

    /// <summary>
    /// I Person Repository.
    /// </summary>
    public interface IPersonDataView : IDataViewBase<PersonModel, HLinkPersonModel, HLinkPersonModelCollection>
    {
        /// <summary>
        /// Gets or sets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        HLinkPersonModel Current
        {
            get; set;
        }

        ///// <summary>
        ///// Gets all as ViewModel.
        ///// </summary>
        ///// <returns>
        ///// </returns>
        //// List<PersonModel> GetAllAsModel();
        ///// <summary>
        ///// Gets the groups by letter.
        ///// </summary>
        ///// <returns>
        ///// List
        ///// </returns>
        //List<CommonGroupInfoCollection<PersonModel>> GetGroupsByLetter { get; }

        /// <summary>
        /// Gets or sets the person data.
        /// </summary>
        /// <value>
        /// The person data.
        /// </value>
        RepositoryModelType<PersonModel, HLinkPersonModel> PersonData
        {
            get; set;
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        HLinkPersonModelCollection GetAllAsHLink();

        /// <summary>
        /// Gets the default image from collection.
        /// </summary>
        /// <param name="argModel">
        /// The argument ViewModel.
        /// </param>
        /// <returns>
        /// </returns>
        HLinkMediaModel GetDefaultImageFromCollection(PersonModel argModel);

        ///// <summary>
        ///// Gets the groups by category.
        ///// </summary>
        ///// <returns>
        ///// List.
        ///// </returns>
        //CommonGroupInfoCollection GetGroupsByCategory();
        /// <summary>
        /// Gets the person plus family events.
        /// </summary>
        /// <param name="argPerson">
        /// The argument person.
        /// </param>
        /// <returns>
        /// Person and where parent in families events.
        /// </returns>
        ObservableCollection<EventModel> GetPersonPlusFamilyEvents(PersonModel argPerson);
    }
}