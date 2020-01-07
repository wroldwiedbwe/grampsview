//-----------------------------------------------------------------------
//
// Various data modesl to small to be worth putting in their own file
// is first launched.
//
// <copyright file="HLinkBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace GrampsView.Data.Model
{
    using System;
    using GrampsView.Common;
    using GrampsView.Data.DataView;

    using System.Collections;
    using System.Globalization;
    using System.Runtime.Serialization;

    /// <summary>
    /// GRAMPS $$(Hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkBase : CommonBindableBase, IHLinkBase
    {
        /// <summary>
        /// The local h link key.
        /// </summary>
        private string localHLinkKey = string.Empty;

        /// <summary>
        /// The local priv.
        /// </summary>
        private bool localPriv = default(bool);

        /// <summary>
        /// Gets the actual ViewModel.
        /// </summary>
        /// <param name="collectionIndex">
        /// Index of the collection.
        /// </param>
        /// <returns>
        /// </returns>
        public ModelBase GetActualModel
        {
            get
            {
                switch (GetType().Name)
                {
                    case "HLinkBookMarkModel":
                        {
                            return DV.BookMarkDV.GetModel(HLinkKey);
                        }

                    case "HLinkCitationModel":
                        {
                            return DV.CitationDV.GetModel(HLinkKey);
                        }

                    case "HLinkEventModel":
                        {
                            return DV.EventDV.GetModel(HLinkKey);
                        }

                    case "HLinkFamilyModel":
                        {
                            return DV.FamilyDV.GetModel(HLinkKey);
                        }

                    case "HLinkMediaModel":
                        {
                            return DV.MediaDV.GetModel(HLinkKey);
                        }

                    case "HLinkNameMapModel":
                        {
                            return DV.NameMapDV.GetModel(HLinkKey);
                        }

                    case "HLinkNoteModel":
                        {
                            return DV.NoteDV.GetModel(HLinkKey);
                        }

                    case "HLinkPersonModel":
                        {
                            return DV.PersonDV.GetModel(HLinkKey);
                        }

                    case "HLinkPlaceModel":
                        {
                            return DV.PlaceDV.GetModel(HLinkKey);
                        }

                    case "HLinkRepositoryModel":
                        {
                            return DV.RepositoryDV.GetModel(HLinkKey);
                        }

                    case "HLinkSourceModel":
                        {
                            return DV.SourceDV.GetModel(HLinkKey);
                        }

                    case "HLinkSourceAttrModel":
                        {
                            // TODO fix this workingCopy.Models.Add(localPersonDataview.Get(item.HLinkKey));
                            break;
                        }

                    case "HLinkTagModel":
                        {
                            return DV.TagDV.GetModel(HLinkKey);
                        }

                    default:
                        break;
                }

                // None of the above (panic)
                new ArgumentException("HLinkBase item is not a known type.  HLinkKey is " + HLinkKey);

                return null;
            }
        }

        [DataMember]
        public bool GPriv
        {
            get
            {
                return localPriv;
            }

            set
            {
                SetProperty(ref localPriv, value);
            }
        }

        /// <summary>
        /// Gets or sets the h link key.
        /// </summary>
        /// <value>
        /// The h link key.
        /// </value>
        [DataMember]
        public string HLinkKey
        {
            get
            {
                return localHLinkKey;
            }

            set
            {
                SetProperty(ref localHLinkKey, value);
            }
        }

        /// <summary>
        /// Gets the priv as string.
        /// </summary>
        /// <value>
        /// The priv as string.
        /// </value>
        public string PrivAsString
        {
            get
            {
                return localPriv.ToString(CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Gets a value indicating whether gets boolean showing if the $$(HLink)$$ is valid.
        /// </summary>
        /// <value>
        /// Boolean showing if $$(HLink)$$ is valid.
        /// </value>
        public virtual bool Valid
        {
            get
            {
                return (!string.IsNullOrEmpty(HLinkKey));
            }
        }

        public static bool operator !=(HLinkBase left, HLinkBase right)
        {
            return !(left == right);
        }

        public static bool operator <(HLinkBase left, HLinkBase right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        public static bool operator <=(HLinkBase left, HLinkBase right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        public static bool operator ==(HLinkBase left, HLinkBase right)
        {
            if (ReferenceEquals(left, null))
            {
                return ReferenceEquals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator >(HLinkBase left, HLinkBase right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        public static bool operator >=(HLinkBase left, HLinkBase right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares the specified x.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// </returns>
        int IComparer.Compare(object x, object y)
        {
            return Compare(x, y);
        }

        /// <summary>
        /// Compares to. Bases it on the HLInkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            return HLinkKey.CompareTo((obj as HLinkBase).HLinkKey);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            return (this.HLinkKey == (obj as HLinkBase).HLinkKey);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the base fields.
        /// </summary>
        /// <param name="arg">
        /// The argument.
        /// </param>
        public void SetBase(HLinkBase arg)
        {
            HLinkKey = arg.HLinkKey;
        }

        /// <summary>
        /// Compares the specified x. Bases it on the HLInkKey for want of anything else that makes sense.
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        /// <param name="y">
        /// The y.
        /// </param>
        /// <returns>
        /// </returns>
        private int Compare(object x, object y)
        {
            return (x as HLinkBase).HLinkKey.CompareTo((y as HLinkBase).HLinkKey);
        }
    }
}