using System;
using Corrections.Ioms.Bpl.Entities;
using Corrections.Ioms.Bpl;

namespace Corrections.Ioms.Bpl.Movement.SharedAccomAssmnt.Entities
{
    [Serializable]
    public class SACRAQACRequestResult : EntityBase
    {
        private string _requestIds = string.Empty;

        public SACRAQACRequestResult()
            : base()
        {
        }

        public SACRAQACRequestResult(RecordRow recordRow)
            : base()
        {
            base.UnpackRequest(recordRow);
        }

        [DataFieldNameAttribute("REQUEST_IDS")]
        public string RequestIds
        {
            get { return _requestIds; }
            set { _requestIds = value; }
        }

        public static EntityBaseCollection<SACRAQACRequestResult> GetSACRAQACRequestResultList(RecordRowCollection recordRowCollection)
        {
            EntityBaseCollection<SACRAQACRequestResult> resultList = new EntityBaseCollection<SACRAQACRequestResult>();

            for (int recordCount = 0; recordCount < recordRowCollection.Count; recordCount++)
            {
                SACRAQACRequestResult result = new SACRAQACRequestResult(recordRowCollection[recordCount]);

                resultList.Add(result);
            }

            return resultList;
        }
    }
}