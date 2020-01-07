// <summary>
// Various routines called to load the entity Home Images
// </summary>
// <copyright file="GrampsStorePostLoadHomeImage.cs" company="PlaceholderCompany">
//     Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GrampsView.Data.ExternalStorageNS
{
    using GrampsView.Common;

    using GrampsView.Data.DataView;

    using GrampsView.Data.Model;
    using GrampsView.Data.Repository;

    using System;

    /// <summary>
    /// Various routines called to load the entity Home Images.
    /// </summary>
    public partial class GrampsStorePostLoad : CommonBindableBase, IStorePostLoad
    {
        /// <summary>
        /// Sets the home h link.
        /// </summary>
        /// <param name="HomeImageHLink">
        /// The home image h link.
        /// </param>
        /// <param name="argHLink">
        /// The hlink.
        /// </param>
        /// <returns>
        /// </returns>
        public static HLinkMediaModel SetHomeHLink(HLinkMediaModel argStartHLink, HLinkMediaModel argHLink)
        {
            if (argStartHLink is null)
            {
                throw new ArgumentNullException(nameof(argStartHLink));
            }

            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            argStartHLink.GCorner1X = argHLink.GCorner1X;
            argStartHLink.GCorner1Y = argHLink.GCorner1Y;
            argStartHLink.GCorner2X = argHLink.GCorner2X;
            argStartHLink.GCorner2Y = argHLink.GCorner2Y;
            argStartHLink.HLinkKey = argHLink.HLinkKey;
            argStartHLink.HomeImageType = argHLink.HomeImageType;

            return argStartHLink;
        }

        /// <summary>
        /// Gets the book mark home images.
        /// </summary>
        public static void GetBookMarkHomeImages()
        {
            foreach (BookMarkModel argModel in DV.BookMarkDV.BookMarkData)
            {
                argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
            }
        }

        /// <summary>
        /// Gets the event home images.
        /// </summary>
        public static void GetEventHomeImages()
        {
            foreach (EventModel argModel in DV.EventDV.EventData)
            {
                if (argModel.Id == "E1196")
                {
                }

                // Try media reference collection first
                HLinkMediaModel hlink = argModel.GMediaRefCollection.FirstHLink;

                // Check Media for Images
                if (hlink is null)
                {
                    hlink = argModel.GMediaRefCollection.FirstHLink;
                }

                // Check Citation for Images
                if (hlink is null)
                {
                    hlink = argModel.GCitationRefCollection.FirstHLink;

                    //hlink = DV.CitationDV.GetFirstImageFromCollection(argModel.GCitationRefCollection);
                }

                // Handle the link if we can
                if (hlink == null)
                {
                    argModel.HomeImageHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;
                }
                else
                {
                    argModel.HomeImageHLink = SetHomeHLink(argModel.HomeImageHLink, hlink);
                }
            }
        }

        /// <summary>
        /// Gets the home images.
        /// </summary>
        public void GetHomeImages()
        {
            // TODO move remaining getHomeImages to Respository Reorg code
            _CL.LogRoutineEntry("GetHomeImages");

            GetBookMarkHomeImages();

            GetEventHomeImages();

            _CL.LogRoutineExit("GetHomeImages");
        }

        /// <summary>
        /// Gets the tag reference home link.
        /// </summary>
        /// <param name="argHLink">
        /// The argument h link.
        /// </param>
        /// <returns>
        /// </returns>
        public static HLinkMediaModel GetTagRefHomeLink(TagModel argModel, HLinkMediaModel argHLink)
        {
            if (argModel is null)
            {
                throw new ArgumentNullException(nameof(argModel));
            }

            if (argHLink is null)
            {
                throw new ArgumentNullException(nameof(argHLink));
            }

            HLinkMediaModel returnHLink = argHLink;

            returnHLink.HomeImageType = CommonConstants.HomeImageTypeSymbol;

            // Set the colour of the tag ref to match the tag
            returnHLink.HomeSymbolColour = argModel.GColor;

            return returnHLink;
        }
    }
}