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
        private ModelBase _ActualModel = null;

        /// <summary>
        /// The local h link key.
        /// </summary>
        private string _HLinkKey = string.Empty;

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
                if (!(_ActualModel is null))
                {
                    return _ActualModel;
                }

                switch (GetType().Name)
                {
                    case "HLinkBookMarkModel":
                        {
                            _ActualModel = DV.BookMarkDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkCitationModel":
                        {
                            _ActualModel = DV.CitationDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkEventModel":
                        {
                            _ActualModel = DV.EventDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkFamilyModel":
                        {
                            _ActualModel = DV.FamilyDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkMediaModel":
                        {
                            _ActualModel = DV.MediaDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkNameMapModel":
                        {
                            _ActualModel = DV.NameMapDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkNoteModel":
                        {
                            _ActualModel = DV.NoteDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkPersonModel":
                        {
                            _ActualModel = DV.PersonDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkPlaceModel":
                        {
                            _ActualModel = DV.PlaceDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkRepositoryModel":
                        {
                            _ActualModel = DV.RepositoryDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkSourceModel":
                        {
                            _ActualModel = DV.SourceDV.GetModel(HLinkKey);
                            break;
                        }

                    case "HLinkSourceAttrModel":
                        {
                            // TODO fix this workingCopy.Models.Add(localPersonDataview.Get(item.HLinkKey));
                            break;
                        }

                    case "HLinkTagModel":
                        {
                            _ActualModel = DV.TagDV.GetModel(HLinkKey);
                            break;
                        }

                    default:

                        // None of the above (panic)
                        throw new ArgumentException("HLinkBase item is not a known type.  HLinkKey is " + HLinkKey);
                }

                return _ActualModel;
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
                return _HLinkKey;
            }

            set
            {
                SetProperty(ref _HLinkKey, value);
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