//-----------------------------------------------------------------------
//
// Various note models
//
// <copyright file="HLinkNoteModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

////<define name = "noteref-content" >
////  < attribute name="hlink">
////    <data type = "IDREF" />
////  </ attribute >
////</ define >

namespace GrampsView.Data.Model
{
    using System;
    using System.Runtime.Serialization;

    using GrampsView.Data.DataView;

    /// <summary>
    /// GRAMPS $$(hlink)$$ element class.
    /// </summary>
    [DataContract]
    public class HLinkBackLink : HLinkBase
    {
        private HLinkBookMarkModel _HLinkBookMarkModel;

        private HLinkCitationModel _HLinkCitationModel;

        private HLinkEventModel _HLinkEventModel;
        private HLinkFamilyModel _HLinkFamilyModel;
        private HLinkMediaModel _HLinkMediaModel;

        private HLinkNameMapModel _HLinkNameMapModel;

        private HLinkNoteModel _HLinkNoteModel;

        private HLinkPersonModel _HLinkPersonModel;

        private HLinkPlaceModel _HLinkPlaceModel;

        private HLinkRepositoryModel _HLinkRepositoryModel;

        private HLinkSourceModel _HLinkSourceModel;

        private HLinkTagModel _HLinkTagModel;

        public HLinkBackLink(HLinkBookMarkModel ArgHLinkLink)
        {
            _HLinkBookMarkModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkBookMarkModel;
        }

        public HLinkBackLink(HLinkCitationModel ArgHLinkLink)
        {
            _HLinkCitationModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkCitationModel;
        }

        public HLinkBackLink(HLinkMediaModel ArgHLinkLink)
        {
            _HLinkMediaModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkMediaModel;
        }

        public HLinkBackLink(HLinkEventModel ArgHLinkLink)
        {
            _HLinkEventModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkEventModel;
        }

        public HLinkBackLink(HLinkFamilyModel ArgHLinkLink)
        {
            _HLinkFamilyModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkFamilyModel;
        }

        public HLinkBackLink(HLinkNameMapModel ArgHLinkLink)
        {
            _HLinkNameMapModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkNameMapModel;
        }

        public HLinkBackLink(HLinkNoteModel ArgHLinkLink)
        {
            _HLinkNoteModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkNoteModel;
        }

        public HLinkBackLink(HLinkPersonModel ArgHLinkLink)
        {
            _HLinkPersonModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkPersonModel;
        }

        public HLinkBackLink(HLinkPlaceModel ArgHLinkLink)
        {
            _HLinkPlaceModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkPlaceModel;
        }

        public HLinkBackLink(HLinkRepositoryModel ArgHLinkLink)
        {
            _HLinkRepositoryModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkRepositoryModel;
        }

        public HLinkBackLink(HLinkSourceModel ArgHLinkLink)
        {
            _HLinkSourceModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkSourceModel;
        }

        public HLinkBackLink(HLinkTagModel ArgHLinkLink)
        {
            _HLinkTagModel = ArgHLinkLink;

            HLinkType = HLinkBackLinkEnum.HLinkTagModel;
        }

        public enum HLinkBackLinkEnum : int
        {
            HLinkBookMarkModel,
            HLinkCitationModel,
            HLinkEventModel,
            HLinkFamilyModel,
            HLinkMediaModel,
            HLinkNameMapModel,
            HLinkNoteModel,
            HLinkPersonModel,
            HLinkPlaceModel,
            HLinkRepositoryModel,
            HLinkSourceAttrModel,
            HLinkSourceModel,
            HLinkTagModel,
            Unknown
        }

        public HLinkBackLinkEnum HLinkType { get; set; }

        public HLinkBase GetHLink()
        {
            switch (HLinkType)
            {
                case HLinkBackLinkEnum.HLinkBookMarkModel:
                    return _HLinkBookMarkModel;

                case HLinkBackLinkEnum.HLinkCitationModel:
                    return _HLinkCitationModel;

                case HLinkBackLinkEnum.HLinkEventModel:
                    return _HLinkEventModel;

                case HLinkBackLinkEnum.HLinkFamilyModel:
                    return _HLinkFamilyModel;

                case HLinkBackLinkEnum.HLinkMediaModel:
                    return _HLinkMediaModel;

                case HLinkBackLinkEnum.HLinkNameMapModel:
                    return _HLinkNameMapModel;

                case HLinkBackLinkEnum.HLinkNoteModel:
                    return _HLinkNoteModel;

                case HLinkBackLinkEnum.HLinkPersonModel:
                    return _HLinkPersonModel;

                case HLinkBackLinkEnum.HLinkPlaceModel:
                    return _HLinkPlaceModel;

                case HLinkBackLinkEnum.HLinkRepositoryModel:
                    return _HLinkRepositoryModel;

                case HLinkBackLinkEnum.HLinkSourceModel:
                    return _HLinkSourceModel;

                case HLinkBackLinkEnum.HLinkTagModel:
                    return _HLinkTagModel;

                case HLinkBackLinkEnum.Unknown:
                    break;

                default:
                    return default(HLinkBase);
            }

            return default(HLinkBase);
        }
    }
}