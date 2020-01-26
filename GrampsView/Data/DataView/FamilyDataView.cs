//-----------------------------------------------------------------------
//
// The Family Repository stores GRAMPSVIEw Family ELements
//
// <copyright file="FamilyDataView.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.DataView
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Data.Collections;
    using GrampsView.Data.Model;
    using GrampsView.Data.Repositories;
    using GrampsView.Data.Repository;

    // The Family Repository </summary>
    public class FamilyDataView : DataViewBase<FamilyModel, HLinkFamilyModel, HLinkFamilyModelCollection>, IFamilyDataView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FamilyDataView" /> class.
        /// </summary>
        public FamilyDataView()
        {
        }

        public override IReadOnlyList<FamilyModel> DataDefaultSort
        {
            get
            {
                return DataViewData.Items.OrderBy(FamilyModel => FamilyModel.FamilyDisplayNameSort).ToList();
            }
        }

        /// <summary>
        /// Gets the data view data.
        /// </summary>
        /// <value>
        /// The data view data.
        /// </value>
        public override RepositoryModelType<FamilyModel, HLinkFamilyModel> DataViewData
        {
            get
            {
                return FamilyData;
            }
        }

        /// <summary>
        /// Gets or sets the family data.
        /// </summary>
        /// <value>
        /// The family data.
        /// </value>
        [DataMember]
        public RepositoryModelType<FamilyModel, HLinkFamilyModel> FamilyData
        {
            get
            {
                return DataStore.DS.localFamilyData;
            }

            set
            {
                SetProperty(ref DataStore.DS.localFamilyData, value);
            }
        }

        public override List<CommonGroupInfoCollection<FamilyModel>> GetGroupsByLetter
        {
            get
            {
                List<CommonGroupInfoCollection<FamilyModel>> groups = new List<CommonGroupInfoCollection<FamilyModel>>();

                var query = from item in FamilyData.Items
                            orderby item.GFather.DeRef.GBirthName.GSurName
                            group item by (item.GFather.DeRef.GBirthName.GSurName + " ").ToUpper(CultureInfo.CurrentCulture).Substring(0, 1) into g
                            select new { GroupName = g.Key, Items = g };

                foreach (var g in query)
                {
                    CommonGroupInfoCollection<FamilyModel> info = new CommonGroupInfoCollection<FamilyModel>
                    {
                        Key = g.GroupName,
                    };

                    foreach (var item in g.Items)
                    {
                        info.Add(item);
                    }

                    groups.Add(info);
                }

                return groups;
            }
        }

        /// <summary>
        /// Gets all as hlink.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkFamilyModelCollection GetAllAsHLink()
        {
            HLinkFamilyModelCollection t = new HLinkFamilyModelCollection();

            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return t;
            }

            foreach (var item in DataDefaultSort)
            {
                t.Add(item.GetHLink);
            }

            t = HLinkCollectionSort(t);

            return t;
        }

        /// <summary>
        /// Gets any children of the family.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkPersonModelCollection GetChildren(HLinkFamilyModel hlinkFamily)
        {
            HLinkPersonModelCollection t = new HLinkPersonModelCollection();

            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return t;
            }

            // TODO fix this
            if (FamilyData.GetModelFromHLink(hlinkFamily).GChildRefCollection.Count > 0)
            {
                t.Add((HLinkPersonModel)FamilyData.GetModelFromHLink(hlinkFamily).GChildRefCollection[0]);
                return t;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Currents the partner.
        /// </summary>
        /// <returns>
        /// </returns>
        public HLinkPersonModel GetCurrentPartner(HLinkFamilyModel hlinkFamily)
        {
            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return null;
            }

            return (HLinkPersonModel)FamilyData.GetModelFromHLink(hlinkFamily).GMother;
        }

        /// <summary>
        /// Currents the spouses.
        /// </summary>
        /// <param name="hlinkFamily">
        /// The hlink family.
        /// </param>
        /// <returns>
        /// </returns>
        public HLinkPersonModelCollection GetCurrentSpouses(HLinkFamilyModel hlinkFamily)
        {
            HLinkPersonModelCollection t = new HLinkPersonModelCollection();

            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return t;
            }

            t.Add((HLinkPersonModel)FamilyData.GetModelFromHLink(hlinkFamily).GMother);
            return t;
        }

        /// <summary>
        /// Gets the father.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        /// <returns>
        /// </returns>
        public IPersonModel GetFather(string arg)
        {
            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return null;
            }

            return GetModel(arg).GFather.DeRef;
        }

        /// <summary>
        /// Gets the mother.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        /// <returns>
        /// </returns>
        public IPersonModel GetMother(string arg)
        {
            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return null;
            }

            return GetModel(arg).GMother.DeRef;
        }

        /// <summary>
        /// hes the link collection sort.
        /// </summary>
        /// <param name="collectionArg">
        /// The collection argument.
        /// </param>
        /// <returns>
        /// Sorted hlink collection.
        /// </returns>
        public override HLinkFamilyModelCollection HLinkCollectionSort(HLinkFamilyModelCollection collectionArg)
        {
            // Handle the case where there is no data.
            if (FamilyData.Count == 0)
            {
                return null;
            }

            if (collectionArg == null)
            {
                return null;
            }

            IOrderedEnumerable<HLinkFamilyModel> t = collectionArg.OrderBy(HLinkFamilyModel => HLinkFamilyModel.DeRef.FamilyDisplayName);

            HLinkFamilyModelCollection tt = new HLinkFamilyModelCollection();

            foreach (HLinkFamilyModel item in t)
            {
                tt.Add(item);
            }

            return tt;
        }

        public override List<SearchItem> Search(string queryString)
        {
            List<SearchItem> itemsFound = new List<SearchItem>();

            var temp = FamilyData.Items.Where(x => x.FamilyDisplayName.ToLower(CultureInfo.CurrentCulture).Contains(queryString));

            foreach (FamilyModel tempMO in temp)
            {
                itemsFound.Add(new SearchItem
                {
                    HLink = tempMO.GetHLink,
                    Text = tempMO.GetDefaultText,
                });
            }

            return itemsFound;
        }
    }
}