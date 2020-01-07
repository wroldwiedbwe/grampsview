// <copyright file="BookMarkModel.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//// COMPLETED
////<define name = "bookmark-content" >
////   < attribute name="target">
////     <choice>
////       <value>person</value>
////       <value>family</value>
////       <value>event</value>
////       <value>source</value>
////       <value>citation</value>
////       <value>place</value>
////       <value>media</value>
////       <value>repository</value>
////       <value>note</value>
////     </choice>
////   </attribute>
////   <attribute name="hlink">
////     <data type="IDREF" />
////   </attribute>
//// </define>

namespace GrampsView.Data.Model
{
    using System;
    using System.Collections;
    using System.Runtime.Serialization;

    using GrampsView.Common;
    using GrampsView.Views;

    /// <summary>
    /// BookMark ViewModel.
    /// ---- Finished.
    /// </summary>
    /// <seealso cref="ModelBase" />
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="IBookMarkModel" />
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="IComparable" />
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="IComparer" />
    [DataContract]
    public sealed class BookMarkModel : ModelBase, IBookMarkModel, IComparable, IComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookMarkModel" /> class.
        /// </summary>
        public BookMarkModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconBookMark;
        }

        /// <summary>
        /// Sets the book mark h link.
        /// </summary>
        /// <value>
        /// The book mark h link.
        /// </value>
        [DataMember]
        public string BookMarkHLink
        {
            private get;
            set;
        }

            = null;

        public string DetailPage { get; set; } = string.Empty;

        /// <summary>
        /// Gets the get book mark h link.
        /// </summary>
        /// <value>
        /// The get book mark h link.
        /// </value>
        public HLinkBase GetBookMarkHLink
        {
            get
            {
                switch (GTarget)
                {
                    case "person":
                        {
                            HLinkPersonModel t = new HLinkPersonModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(PersonDetailPage);

                            return t;
                        }

                    case "family":
                        {
                            HLinkFamilyModel t = new HLinkFamilyModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(FamilyDetailPage);

                            return t;
                        }

                    case "event":
                        {
                            HLinkEventModel t = new HLinkEventModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(EventDetailPage);

                            return t;
                        }

                    case "source":
                        {
                            HLinkSourceModel t = new HLinkSourceModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(SourceDetailPage);

                            return t;
                        }

                    case "citation":
                        {
                            HLinkCitationModel t = new HLinkCitationModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(CitationDetailPage);

                            return t;
                        }

                    case "place":
                        {
                            HLinkPlaceModel t = new HLinkPlaceModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(PlaceDetailPage);

                            return t;
                        }

                    case "media":
                        {
                            HLinkMediaModel t = new HLinkMediaModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(MediaDetailPage);

                            return t;
                        }

                    case "repository":
                        {
                            HLinkRepositoryModel t = new HLinkRepositoryModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(RepositoryDetailPage);

                            return t;
                        }

                    case "note":
                        {
                            HLinkNoteModel t = new HLinkNoteModel
                            {
                                HLinkKey = BookMarkHLink,
                            };

                            DetailPage = nameof(NoteDetailPage);

                            return t;
                        }
                }

                // TODO handle error
                return null;
            }
        }

        /// <summary>
        /// Gets the get h link.
        /// </summary>
        /// <value>
        /// The get h link.
        /// </value>
        public HLinkBookMarkModel GetHLink
        {
            get
            {
                HLinkBookMarkModel t = new HLinkBookMarkModel
                {
                    HLinkKey = HLinkKey,
                };
                return t;
            }
        }

        /// <summary>
        /// Gets or sets the target attribute.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        [DataMember]
        public string GTarget
        {
            get;
            set;
        }

            = null;

        public string TargetDecoded
        {
            get
            {
                return GTarget[0].ToString().ToUpper()
                    + GTarget.Substring(1);
            }
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator !=(BookMarkModel left, BookMarkModel right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Implements the operator &lt;.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <(BookMarkModel left, BookMarkModel right)
        {
            return left is null ? !(right is null) : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the operator &lt;=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator <=(BookMarkModel left, BookMarkModel right)
        {
            return left is null || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator ==(BookMarkModel left, BookMarkModel right)
        {
            if (left is null)
            {
                return right is null;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator &gt;.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >(BookMarkModel left, BookMarkModel right)
        {
            return !(left is null) && left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the operator &gt;=.
        /// </summary>
        /// <param name="left">
        /// The left.
        /// </param>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// The result of the operator.
        /// </returns>
        public static bool operator >=(BookMarkModel left, BookMarkModel right)
        {
            return left is null ? right is null : left.CompareTo(right) >= 0;
        }

        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="a">
        /// object A.
        /// </param>
        /// <param name="b">
        /// object B.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparer.Compare(object a, object b)
        {
            BookMarkModel firstEvent = (BookMarkModel)a;
            BookMarkModel secondEvent = (BookMarkModel)b;

            // compare on Priority first
            int testFlag = string.Compare(firstEvent.GTarget, secondEvent.GTarget, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Implement IComparable CompareTo method.
        /// </summary>
        /// <param name="obj">
        /// The object to compare.
        /// </param>
        /// <returns>
        /// One, two or three.
        /// </returns>
        int IComparable.CompareTo(object obj)
        {
            BookMarkModel secondEvent = (BookMarkModel)obj;

            // compare on Target first
            int testFlag = string.Compare(GTarget, secondEvent.GTarget, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Compares to.
        /// </summary>
        /// <param name="right">
        /// The right.
        /// </param>
        /// <returns>
        /// </returns>
        public int CompareTo(BookMarkModel right)
        {
            int testFlag = string.Compare(GTarget, right.GTarget, StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="object" /> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c> true </c> if the specified <see cref="object" /> is equal to this instance;
        /// otherwise, <c> false </c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (!GetHLink.Valid || !(obj is BookMarkModel))
            {
                return false;
            }

            if (GetHLink == (obj as BookMarkModel).GetHLink)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures
        /// like a hash table.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}