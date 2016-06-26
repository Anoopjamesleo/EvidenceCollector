using System;
using Microsoft.Office.Interop.Word;
using System.Collections.Generic;

namespace EvidenceCollector
{



    class Evidence
    {

        public class ActualEvidence
        {
            public string strStatus, strComments;
            string strEvidenceID, strEvidenceImageURI;
            int iEvidenceOrdinal;

            public ActualEvidence(string strEvidenceID, int iEvidenceOrdinal, string strEvidenceImageURI, string strStatus, string strComments)
            {
                this.strStatus = strStatus;
                this.strComments = strComments;
                this.strEvidenceID = strEvidenceID;
                this.strEvidenceImageURI = strEvidenceImageURI;
                this.iEvidenceOrdinal = iEvidenceOrdinal;
            }

            public string strPropEvidenceID
            {
                get
                {
                    return strEvidenceID;
                }
            }


            public string strPropEvidenceImageURI
            {
                get
                {
                    return strEvidenceImageURI;
                }
            }
        }


        string strTestPlanName, strSolutions, strEnvironment, strOperatingSystem, strDomain, strAssociateID, strTestData;
        DateTime dtCreatedDate;
        int iNumberOfEvidences;
        // int iEvidencesCaptured;
        string strEvidenceString;
        Table tbEvidenceTableFormat;
        // ActualEvidence[] actActualEvidences;
        Dictionary<string, ActualEvidence> dictActualEvidence;



        public Evidence(string strTestPlanName, int iNumberOfSteps)
        {
            this.strTestPlanName = strTestPlanName;
            this.iNumberOfEvidences = iNumberOfSteps;
            // actActualEvidences = new ActualEvidence[iNumberOfSteps];
            dictActualEvidence = new Dictionary<string, ActualEvidence>();
            // iEvidencesCaptured = 0;
        }


        /// <summary>
        /// Add an acutal evidence captured and it's details to the Evidence class object.
        /// </summary>
        /// <param name="iStepNumber"></param>
        /// <param name="strStatus"></param>
        /// <param name="strComments"></param>
        /// <param name="strEvidenceID"></param>
        /// <param name="strEvidenceImageName"></param>
        public void AddActualEvidence(string strEvidenceID, int iEvidenceOrdinal, string strEvidenceImageName, string strStatus, string strComments)
        {
            dictActualEvidence[strEvidenceID] = new ActualEvidence(strEvidenceID, iEvidenceOrdinal, strEvidenceImageName, strStatus, strComments);
        }




        /// <summary>
        /// Add solutions details.
        /// </summary>
        /// <param name="strSolutions"></param>
        /// <param name="strTestData"></param>
        /// <param name="strDomain"></param>
        /// <param name="strEnvironment"></param>
        /// <param name="strOperatingSystem"></param>
        /// <param name="strAssociateID"></param>
        public void AddDetails(string strSolutions, string strTestData, string strDomain, string strEnvironment, string strOperatingSystem, string strAssociateID)
        {
            this.strSolutions = strSolutions;
            this.strTestData = strTestData;
            this.strDomain = strDomain;
            this.strEnvironment = strEnvironment;
            this.strOperatingSystem = strOperatingSystem;
            this.strAssociateID = strAssociateID;
        }

        /// <summary>
        /// Add details of the template file obtained by parsing the template file.
        /// </summary>
        /// <param name="tbEvidenceTableFormat"></param>
        /// <param name="strEvidenceString"></param>
        public void AddTemplateDetails(Table tbEvidenceTableFormat, string strEvidenceString)
        {
            this.tbEvidenceTableFormat = tbEvidenceTableFormat;
            this.strEvidenceString = strEvidenceString;
        }


        /// <summary>
        /// Fetch system information during run time from the system.
        /// Information collected:
        /// Date of execution
        /// </summary>
        public void FetchDetails()
        {
            dtCreatedDate = DateTime.UtcNow.Date;
        }



        public string strPropTestPlanName
        {
            get
            {
                return strTestPlanName;
            }
        }
        public string strPropSolutions
        {
            get
            {
                return strSolutions;
            }

        }
        public string strPropEnvironment
        {
            get
            {
                return strEnvironment;
            }
        }
        public string strPropOperatingSystem
        {
            get
            {
                return strOperatingSystem;
            }
        }
        public string strPropDomain
        {
            get
            {
                return strDomain;
            }
        }
        public string strPropAssociateID
        {
            get
            {
                return strAssociateID;
            }
        }
        public string strPropTestData
        {
            get
            {
                return strTestData;
            }
        }
        public DateTime dtPropCreatedDate
        {
            get
            {
                return dtCreatedDate;
            }
        }
        public int iPropNumberOfEvidences
        {
            get
            {
                return iNumberOfEvidences;
            }
        }

        public string strPropEvidenceString
        {
            get
            {
                return strEvidenceString;
            }
        }
        public Table tbPropEvidenceTableFormat
        {
            get
            {
                return tbEvidenceTableFormat;
            }
        }


        public Dictionary<string, ActualEvidence> dictPropActualEvidences
        {
            get
            {
                return dictActualEvidence;
            }
        }

    }
}
