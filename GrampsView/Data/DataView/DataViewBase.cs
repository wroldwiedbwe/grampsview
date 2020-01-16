// <copyright file="DataViewBase.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.DataView
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;

    using GrampsView.Common;

    using GrampsView.Data.Model;

    using GrampsView.Data.Repositories;

    /// <summary>
    /// Partially based on http://stackoverflow.com/questions/8157140/net-4-0-indexer-with-observablecollection.
    /// </summary>
    /// <typeparam name="TB">
    /// HLinkCollection.
    /// </typeparam>
    /// <typeparam name="TU">
    /// ModelBase.
    /// </typeparam>
    /// <typeparam name="TH">
    /// Hlink.
    /// </typeparam>
    /// <seealso cref="GrampsView.Common.CommonBindableBase" />
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="GrampsView.Data.DataView.IDataViewBase{T, U, H}" />
    /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="System.ComponentViewModel.INotifyPropertyChanged" />
    public abstract class DataViewBase<TB, TU, TH> : CommonBindableBase, IDataViewBase<TB, TU, TH>, INotifyPropertyChanged
        where TH : HLinkBaseCollection<TU>, new()
        where TB : ModelBase, new()
        where TU : HLinkBase, new()
    {
        public int Count
        {
            get
            {
                return DataViewData.Count;
            }
        }

        /// <summary>
        /// Gets the data default sort.
        /// </summary>
        /// <value>
        /// The data default sort.
        /// </value>
        public abstract IReadOnlyList<TB> DataDefaultSort
        {
            get;
        }

        /// <summary>
        /// Gets or sets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public virtual RepositoryModelType<TB, TU> DataViewData
        {
            get;
            set;
        }

        /// <summary>Gets the get groups by letter.
        /// Default to empty list.</summary>
        /// <value>The get groups by letter.</value>
        public virtual List<CommonGroupInfoCollection<TB>> GetGroupsByLetter
        {
            get
            {
                return new List<CommonGroupInfoCollection<TB>>();
            }
        }

        public virtual CardGroup AsCardGroup(IReadOnlyList<TB> argReadOnlyList)
        {
            CardGroup t = new CardGroup();

            t.Cards.AddRange(new ObservableCollection<object>(argReadOnlyList));

            return t;
        }

        //public TH AsHLinks(IReadOnlyList<TB> argReadOnlyList)
        //{
        //    TH returnCollection = new TH();

        //    foreach (TB item in argReadOnlyList)
        //    {
        //        returnCollection.Add(item.HLink as TU);
        //    }

        //    return returnCollection;
        //}

        /// <summary>
        /// Gets all as ViewModel.
        /// </summary>
        /// <returns>
        /// List of models.
        /// </returns>
        public List<TB> GetAllAsModel()
        {
            return DataViewData.Items.OrderBy(t => t).ToList();
        }

        /// <summary>
        /// Gets the first image from collection.
        /// </summary>
        /// <param name="theCollection">
        /// The collection.
        /// </param>
        /// <returns>
        /// A HLink Media Model of the first image in the collection or null if none are found.
        /// </returns>
        public virtual HLinkMediaModel GetFirstImageFromCollection(TH theCollection)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the specified h link string.
        /// </summary>
        /// <param name="hLinkString">
        /// The h link string.
        /// </param>
        /// <returns>
        /// ModelBase.
        /// </returns>
        public TB GetHLink(string hLinkString)
        {
            return DataViewData.GetModelFromHLink(hLinkString);
        }

        /// <summary>
        /// Gets the specified h link string.
        /// </summary>
        /// <param name="HLinkString">
        /// The h link string.
        /// </param>
        /// <returns>
        /// ModelBase.
        /// </returns>
        public virtual TB GetModel(string HLinkString)
        {
            return DataViewData.GetModelFromHLink(HLinkString);
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="argHLink">
        /// The hlink.
        /// </param>
        /// <returns>
        /// Model for HLink.
        /// </returns>
        public virtual TB GetModel(HLinkBase argHLink)
        {
            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            return DataViewData.GetModelFromHLink(argHLink.HLinkKey);
        }

        /// <summary>
        /// Gets the model information formatted.
        /// </summary>
        /// <param name="argModel">
        /// The argument ViewModel.
        /// </param>
        /// <returns>
        /// A Card List Model object.
        /// </returns>
        public CardListLineCollection GetModelInfoFormatted(ModelBase argModel)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            CardListLineCollection
               modelInfoList = new CardListLineCollection
               {
                 new CardListLine("Handle:", argModel.Handle),
                 new CardListLine("Id:", argModel.Id),
                 new CardListLine("Change:", argModel.Change),
                 new CardListLine("Private Object:", argModel.PrivAsString),
               };

            return modelInfoList;
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// The argument unsorted.
        /// </returns>
        public virtual TH HLinkCollectionSort(TH collectionArg)
        {
            return collectionArg;
        }

        /// <summary>
        /// New instance of ViewModel.
        /// </summary>
        /// <returns>
        /// New model instance.
        /// </returns>
        public virtual TB NewModel()
        {
            TB t = new TB();

            return t;
        }

        /// <summary>
        /// Searches the items.
        /// </summary>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// </returns>
        public abstract List<SearchItem> Search(string queryString);
    }
}