namespace GrampsView.Common
{
    using GrampsView.Data.Model;
    using GrampsView.UserControls;

    public static class CardGroupUtil
    {
        public static MediaImageFullCard GetMediaImageFullCard(HLinkHomeImageModel argImageHLink)
        {
            MediaImageFullCard personImage = new MediaImageFullCard();
            personImage.BindingContext = argImageHLink;

            return personImage;
        }

        public static NoteCardFull GetNoteCardFull(HLinkNoteModel argSourceHLink)
        {
            NoteCardFull bioCard = new NoteCardFull();
            bioCard.BindingContext = argSourceHLink;

            return bioCard;
        }

        public static SourceCardSmall GetSourceCardSmall(HLinkSourceModel argSourceHLink)
        {
            SourceCardSmall sourceCard = new SourceCardSmall();
            sourceCard.BindingContext = argSourceHLink;

            return sourceCard;
        }
    }
}