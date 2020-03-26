using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ABMS_Backend.Models;


namespace ABMS_Backend.DataAccess
{
    public class MonitorDataAccess : IDisposable
    {


        private string conStr = string.Empty;
        public SqlParameter[] sqlParameters { get; set; }
        public Query query { get; set; }


        private Func<object, string> GetStr = (x) => x == null ? string.Empty : x.ToString();
        public MonitorDataAccess()
        {
            conStr = Properties.Resources.Weepay;
        }

        //Return List to a Financial Message
        public List<FinancialMessage> GetFinacialMessages()
        {
            try
            {
                using (var sqlCon = new SqlConnection(conStr))
                {
                    sqlCon.Open();
                    using (var sqlCom = new SqlCommand(SQLQueries(query), sqlCon))
                    {
                        sqlCom.CommandTimeout = 0;
                        sqlCom.Parameters.AddRange(sqlParameters);
                        using (var reader = sqlCom.ExecuteReader())
                        {
                            var tmpList = new List<FinancialMessage>();
                            while (reader.Read())
                            {
                                var tempAmount = double.Parse(reader.GetString(10));
                                var tempFee1 = int.Parse(reader.GetString(25).Trim());
                                var tempFeeType = reader.IsDBNull(21) ? string.Empty : reader.GetString(21);
                                var fltmdrAmount = GetFLTMDRAmount(tempAmount, tempFee1, tempFeeType);
                                tmpList.Add(new FinancialMessage
                                {
                                    Insert_DateTime = reader.GetDateTime(0).ToString("MM-dd-yyyy HH:mm:ss"),
                                    MerchantId = reader.IsDBNull(1) ? "0" : reader.GetString(1),
                                    Terminal = reader.GetString(2),
                                    BIN = reader.GetString(3),
                                    Trx_Type = reader.GetString(4),
                                    Message_Type = reader.GetString(5),
                                    Message_Mode = reader.GetString(6),
                                    Terminal_Type = reader.IsDBNull(7) ? string.Empty : reader.GetString(7),
                                    Entry_Mode = reader.GetString(8),
                                    PIN_Mode = reader.GetString(9),
                                    Amount = reader.GetString(10),
                                    Response = reader.GetString(11),
                                    Code = reader.IsDBNull(12) ? string.Empty : reader.GetString(12),//TransactionErrorCode
                                    ResponceCodeText = reader.IsDBNull(13) ? string.Empty : reader.GetString(13),
                                    STAN = reader.GetString(14),
                                    Serial_No = reader.IsDBNull(15) ? string.Empty : reader.GetString(15),
                                    RefNo = reader.GetInt32(16),
                                    Ori_RefNo = reader.GetString(17),
                                    PHID = reader.GetString(18),
                                    ConnectorName = reader.GetString(19),
                                    MerchantName = reader.IsDBNull(20) ? string.Empty : reader.GetString(20),
                                    FeeType = reader.IsDBNull(21) ? string.Empty : reader.GetString(21).Trim(),
                                    TerminalId = reader.IsDBNull(22) ? string.Empty : reader.GetString(22),
                                    TransactionType = reader.GetString(23),
                                    IssuingBank = reader.IsDBNull(24) ? string.Empty : reader.GetString(24),
                                    FLTMDRAmount = fltmdrAmount

                                });
                            }
                            return tmpList;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetFLTMDRAmount(double amount, int rate, string feeType)
        {
            double temp = 0;
            try
            {
                if (feeType == FeeType.MDR.ToString())
                {
                    var tempRate = rate * 0.01;
                    var res = amount * tempRate;
                    temp = res;
                }
            }
            catch (Exception)
            {
                throw;
            }
            var tempp = temp.ToString("N", new CultureInfo("en-US"));
            return tempp;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    conStr = string.Empty;
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~MonitoringDataAccess()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

        private string SQLQueries(Query query)
        {
            string temp = string.Empty;
            switch (query)
            {
                case Query.getfinancialmessage:
                    temp = @"SELECT * FROM [Ogamon].[Finacial_Message] WHERE
                        Insert_DateTime > @dateFrom and Insert_DateTime < @dateTo
                        AND Message_Mode = 'Response'
                        ORDER By Insert_DateTime DESC";
                    break;
                case Query.getcurrentfinancialmessage:
                    temp = @"SELECT * FROM [Ogamon].[Finacial_Message] WHERE
                        CAST(Insert_DateTime as date) = @dateFrom
                        AND Message_Mode = 'Response'
                        ORDER By Insert_DateTime DESC";
                    break;
                case Query.sgp_SelectMerchantsAll:
                    temp = "";
                    break;
            }
            return temp;
        }
    }

    public enum Query
    {
        getfinancialmessage, getcurrentfinancialmessage, sgp_SelectMerchantsAll
    }

    public enum FeeType
    {
        MDR, FLT
    }
}
