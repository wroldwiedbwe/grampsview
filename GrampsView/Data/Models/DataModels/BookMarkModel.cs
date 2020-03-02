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
    /// <seealso cref="ModelBase"/>
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="IBookMarkModel"/>
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="IComparable"/>
    /// /// /// /// /// /// /// /// /// /// /// /// ///
    /// <seealso cref="IComparer"/>
    [DataContract]
    public sealed class BookMarkModel : ModelBase, IBookMarkModel, IComparable, IComparer
    {
        private HLinkBackLink _HLinkBookMarkTarget = new HLinkBackLink();

        /// <summary>
        /// Initializes a new instance of the <see cref="BookMarkModel"/> class.
        /// </summary>
        public BookMarkModel()
        {
            HomeImageHLink.HomeSymbol = CommonConstants.IconBookMark;
        }

        //public string DetailPage { get; set; } = string.Empty;

        public override string GetDefaultText
        {
            get
            {
                return HLinkBookMarkTarget.HLink().ToString();
            }
        }

        public HLinkBookMarkModel HLink
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
        /// Sets the book mark h link.
        /// </summary>
        /// <value>
        /// The book mark h link.
        /// </value>
        [DataMember]
        public HLinkBackLink HLinkBookMarkTarget
        {
            get
            {
                return _HLinkBookMarkTarget;
            }

            set
            {
                SetProperty(ref _HLinkBookMarkTarget, value);
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
            int testFlag = string.Compare(firstEvent.HLinkBookMarkTarget.ToString(), secondEvent.HLinkBookMarkTarget.ToString(), StringComparison.CurrentCulture);

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
            int testFlag = string.Compare(HLinkBookMarkTarget.ToString(), secondEvent.HLinkBookMarkTarget.ToString(), StringComparison.CurrentCulture);

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
            int testFlag = string.Compare(HLinkBookMarkTarget.ToString(), right.HLinkBookMarkTarget.ToString(), StringComparison.CurrentCulture);

            return testFlag;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/>, is equal to this instance.
        /// </summary>
        /// <param name="obj">
        /// The <see cref="object"/> to compare with this instance.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
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

            if (!Valid || !(obj is BookMarkModel))
            {
                return false;
            }

            if (HLinkBookMarkTarget.ToString() == (obj as BookMarkModel).HLinkBookMarkTarget.ToString())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data
        /// structures like a hash table.
        /// </returns>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the get book mark h link.
        /// </summary>
        /// <value>
        /// The get book mark h link.
        /// </value>
    }
}