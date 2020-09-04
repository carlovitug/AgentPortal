using ABMS_Backend.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ABMS_Backend.Utils
{
    public class Excel
    {
        public async Task<Tuple<MemoryStream, string>> GenerateDailyAcquirer(List<vw_Transactions> daTxn)
        {
            string fName = string.Empty;
            //DeleteUsedTemplate("acquirer"); 
            var dupName = await DuplicateTemplate("acquirer");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Generated", dupName);
            FileInfo file = new FileInfo(filePath);
            MemoryStream ms = new MemoryStream();

            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;
                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
                ExcelWorksheet terminalTemplateWS = excelWorkBook.Worksheets[1];

                #region RANGE INDEXES - HEAD
                int startIndex = 21;

                string hr_Terminal = string.Format("B{0}:E{0}", startIndex - 1);
                string cr_TerminalNo = string.Format("F{0}:I{0}", startIndex - 1);

                string hr_Srno = string.Format("B{0}:C{0}", startIndex);
                string hr_TransactionId = string.Format("D{0}:E{0}", startIndex);
                string hr_SystemTraceAuditNumber = string.Format("F{0}:J{0}", startIndex);
                string hr_Date = string.Format("K{0}:L{0}", startIndex);
                string hr_Time = string.Format("M{0}:O{0}", startIndex);
                string hr_AccountNumber = string.Format("P{0}:R{0}", startIndex);
                string hr_TransDesc = string.Format("S{0}:T{0}", startIndex);
                string hr_Account = string.Format("U{0}", startIndex);
                string hr_TxnAmount = string.Format("V{0}:W{0}", startIndex);

                string cr_Srno = string.Format("B{0}:C{0}", startIndex);
                string cr_TransactionId = string.Format("D{0}:E{0}", startIndex);
                string cr_SystemTraceAuditNumber = string.Format("F{0}:J{0}", startIndex);
                string cr_Date = string.Format("K{0}:L{0}", startIndex);
                string cr_Time = string.Format("M{0}:O{0}", startIndex);
                string cr_AccountNumber = string.Format("P{0}:R{0}", startIndex);
                string cr_TransDesc = string.Format("S{0}:T{0}", startIndex);
                string cr_Account = string.Format("U{0}", startIndex);
                string cr_TxnAmount = string.Format("V{0}:W{0}", startIndex);


                #endregion

                #region CELL - DATA
                string c_Terminal = String.Format("B{0}", startIndex - 1);
                string c_Srno = String.Format("B{0}", startIndex);
                string c_TransactionId = String.Format("D{0}", startIndex);
                string c_SystemTraceAuditNumber = String.Format("F{0}", startIndex);
                string c_Date = String.Format("K{0}", startIndex);
                string c_Time = String.Format("M{0}", startIndex);
                string c_AccountNumber = String.Format("P{0}", startIndex);
                string c_TransDesc = String.Format("S{0}", startIndex);
                string c_Account = String.Format("U{0}", startIndex);
                string c_TxnAmount = String.Format("V{0}", startIndex);


                #endregion

                #region HEADER
                excelWorksheet.Cells["S4"].Value = daTxn[0].TransactionDate;
                excelWorksheet.Cells["E6"].Value = DateTime.Now.ToString("MM/dd/yyyy");
                excelWorksheet.Cells["E7"].Value = DateTime.Now.ToString("HH:mm:ss");
                #endregion


                var terminalList = daTxn.Select(s => s.TerminalNo).Distinct().ToList();

                //Transaction per terminal
                for (int i = 0; i < terminalList.Count; i++)
                {
                    int first = 0;
                    for (int j = 0; j < daTxn.Count; j++)
                    {
                        if (terminalList[i] == daTxn[j].TerminalNo)
                        {

                            #region RANGE INDEXES
                            if (i != 0 && first == 0)
                            {
                                startIndex = startIndex + 3;
                            }

                            int thIndex = startIndex;
                            int valIndex = startIndex + 1;

                            //table header
                            #region TABLE HEADER INDEXES

                            hr_Srno = string.Format("B{0}:C{0}", thIndex);
                            hr_TransactionId = string.Format("D{0}:E{0}", thIndex);
                            hr_SystemTraceAuditNumber = string.Format("F{0}:J{0}", thIndex);
                            hr_Date = string.Format("K{0}:L{0}", thIndex);
                            hr_Time = string.Format("M{0}:O{0}", thIndex);
                            hr_AccountNumber = string.Format("P{0}:R{0}", thIndex);
                            hr_TransDesc = string.Format("S{0}:T{0}", thIndex);
                            hr_Account = string.Format("U{0}", thIndex);
                            hr_TxnAmount = string.Format("V{0}:W{0}", thIndex);
                            #endregion

                            //value index
                            #region CELL VALUES INDEXES
                            cr_Srno = string.Format("B{0}:C{0}", valIndex);
                            cr_TransactionId = string.Format("D{0}:E{0}", valIndex);
                            cr_SystemTraceAuditNumber = string.Format("F{0}:J{0}", valIndex);
                            cr_Date = string.Format("K{0}:L{0}", valIndex);
                            cr_Time = string.Format("M{0}:O{0}", valIndex);
                            cr_AccountNumber = string.Format("P{0}:R{0}", valIndex);
                            cr_TransDesc = string.Format("S{0}:T{0}", valIndex);
                            cr_Account = string.Format("U{0}", valIndex);
                            cr_TxnAmount = string.Format("V{0}:W{0}", valIndex);
                            #endregion


                            if (first == 0)
                            {
                                //adjustement for the next terminal number

                                //copy cell format from row 11
                                excelWorksheet.InsertRow(startIndex - 1, 1, 10); //Terminal label-header in-line
                                //copy cell format from row 11
                                excelWorksheet.InsertRow(startIndex, 1, 11); //Cell header

                                #region TABLE HEADER INDEXES
                                hr_Terminal = string.Format("B{0}:E{0}", startIndex - 1);
                                cr_TerminalNo = string.Format("F{0}:I{0}", startIndex - 1);
                                #endregion

                                #region MERGING OF HEADER
                                excelWorksheet.Cells[hr_Terminal].Merge = true;//terminal header
                                excelWorksheet.Cells[cr_TerminalNo].Merge = true;//termial header

                                excelWorksheet.Cells[hr_Srno].Merge = true;//SRNO
                                excelWorksheet.Cells[hr_TransactionId].Merge = true;//TRACE
                                excelWorksheet.Cells[hr_SystemTraceAuditNumber].Merge = true;//ATM SEQ
                                                                                             //RETRIEVAL REF #  - no merging
                                excelWorksheet.Cells[hr_Date].Merge = true;//DATE
                                excelWorksheet.Cells[hr_Time].Merge = true;//TIME
                                excelWorksheet.Cells[hr_AccountNumber].Merge = true;//CARD NUMBER
                                excelWorksheet.Cells[hr_TransDesc].Merge = true;//TRANS DESC
                                                                                //ACCOUNT - NO MERGE
                                                                                //RESP CODE - NO MERGE
                                                                                //REMARKS - NO MERGE
                                excelWorksheet.Cells[hr_TxnAmount].Merge = true;//TXN AMOUNT
                                                                                //excelWorksheet.Cells[hr_AmtAuth].Merge = true;//AMT AUTH
                                #endregion

                                #region CELL - DATA


                                c_Terminal = String.Format("B{0}", startIndex - 1);
                                c_Srno = String.Format("B{0}", startIndex);
                                c_TransactionId = String.Format("D{0}", startIndex);
                                c_SystemTraceAuditNumber = String.Format("F{0}", startIndex);
                                c_Date = String.Format("K{0}", startIndex);
                                c_Time = String.Format("M{0}", startIndex);
                                c_AccountNumber = String.Format("P{0}", startIndex);
                                c_TransDesc = String.Format("S{0}", startIndex);
                                c_Account = String.Format("U{0}", startIndex);
                                c_TxnAmount = String.Format("V{0}", startIndex);
                                #endregion

                                #region HEADER LABELS
                                excelWorksheet.Cells[c_Terminal].Value = "TERMINAL : ";
                                excelWorksheet.Cells[c_Srno].Value = "SRNO";
                                excelWorksheet.Cells[c_TransactionId].Value = "Transaction Id";
                                excelWorksheet.Cells[c_SystemTraceAuditNumber].Value = "System Trace Audit Number";
                                excelWorksheet.Cells[c_Date].Value = "DATE";
                                excelWorksheet.Cells[c_Time].Value = "TIME";
                                excelWorksheet.Cells[c_AccountNumber].Value = "Account NUMBER";
                                excelWorksheet.Cells[c_TransDesc].Value = "TRANS DESC";
                                excelWorksheet.Cells[c_Account].Value = "ACCOUNT";
                                excelWorksheet.Cells[c_TxnAmount].Value = "TXN AMOUNT";
                                #endregion

                            }
                            //copy cell format from row 12
                            excelWorksheet.InsertRow(startIndex + 1, 1, 12);

                            #region MERGING OF ROW CELLS
                            excelWorksheet.Cells[cr_Srno].Merge = true;//SRNO
                            excelWorksheet.Cells[cr_TransactionId].Merge = true;//TRACE
                            excelWorksheet.Cells[cr_SystemTraceAuditNumber].Merge = true;//ATM SEQ
                                                                                         //RETRIEVAL REF #  - no merging
                            excelWorksheet.Cells[cr_Date].Merge = true;//DATE
                            excelWorksheet.Cells[cr_Time].Merge = true;//TIME
                            excelWorksheet.Cells[cr_AccountNumber].Merge = true;//CARD NUMBER
                            excelWorksheet.Cells[cr_TransDesc].Merge = true;//TRANS DESC
                                                                            //ACCOUNT - NO MERGE
                                                                            //RESP CODE - NO MERGE
                                                                            //REMARKS - NO MERGE
                            excelWorksheet.Cells[cr_TxnAmount].Merge = true;//TXN AMOUNT
                            #endregion

                            #region ROW VALUE
                            excelWorksheet.Cells[cr_TerminalNo].Value = daTxn[j].TerminalNo;
                            excelWorksheet.Cells[cr_Srno].Value = daTxn[j].SRNO;
                            excelWorksheet.Cells[cr_TransactionId].Value = daTxn[j].Trace;
                            excelWorksheet.Cells[cr_SystemTraceAuditNumber].Value = daTxn[j].ATM_SEQ;
                            excelWorksheet.Cells[cr_Date].Value = daTxn[j].TransactionDate;
                            excelWorksheet.Cells[cr_Time].Value = daTxn[j].TransactionTime;
                            excelWorksheet.Cells[cr_AccountNumber].Value = daTxn[j].CardNumber;
                            excelWorksheet.Cells[cr_TransDesc].Value = daTxn[j].TRANS_DESC;
                            excelWorksheet.Cells[cr_Account].Value = daTxn[j].Account;
                            excelWorksheet.Cells[cr_TxnAmount].Value = daTxn[j].TransactionAmount;
                            #endregion

                            #endregion

                            first++;
                            startIndex++;
                        }
                    }
                }
                //Summary
                for (int k = 0; k < terminalList.Count; k++)
                {
                    if (k == 0)
                    {
                        //copy cell format from row 15
                        excelWorksheet.InsertRow(startIndex + 3, 1, 15); //Summary
                        excelWorksheet.InsertRow(startIndex + 4, 1, 17); //Summary

                        string summarySummary = string.Format("C{0}:H{0}", startIndex + 3);
                        string summaryTerminal = string.Format("C{0}:F{0}", startIndex + 4);
                        string summaryCount = string.Format("G{0}:I{0}", startIndex + 4);
                        string summaryAmount = string.Format("J{0}:L{0}", startIndex + 4);
                        string summaryBill = string.Format("M{0}:N{0}", startIndex + 4);

                        excelWorksheet.Cells[summarySummary].Merge = true;//Summary merge
                        excelWorksheet.Cells[summaryTerminal].Merge = true;//Summary Terminal merge
                        excelWorksheet.Cells[summaryCount].Merge = true;//Summary count merge
                        excelWorksheet.Cells[summaryAmount].Merge = true;//Summary amount merge
                        excelWorksheet.Cells[summaryBill].Merge = true;//Summary Bill Ccy merge

                        excelWorksheet.Cells[summarySummary].Value = "SUMMARY";
                        excelWorksheet.Cells[summaryTerminal].Value = "TERMINAL";
                        excelWorksheet.Cells[summaryCount].Value = "COUNT";
                        excelWorksheet.Cells[summaryAmount].Value = "AMOUNT";
                        excelWorksheet.Cells[summaryBill].Value = "BILL CCY";
                    }
                    var temp = startIndex + 5 + k;
                    excelWorksheet.InsertRow(temp, 1, 18); //Summary cell format
                    excelWorksheet.Cells[string.Format("C{0}:F{0}", temp)].Merge = true;//Summary Terminal merge
                    excelWorksheet.Cells[string.Format("G{0}:I{0}", temp)].Merge = true;//Summary count merge
                    excelWorksheet.Cells[string.Format("J{0}:L{0}", temp)].Merge = true;//Summary amount merge
                    excelWorksheet.Cells[string.Format("M{0}:N{0}", temp)].Merge = true;//Summary Bill Ccy merge
                    //terminal number
                    excelWorksheet.Cells[string.Format("C{0}:F{0}", temp)].Value = terminalList[k];
                    //Count
                    excelWorksheet.Cells[string.Format("G{0}:I{0}", temp)].Value = daTxn.Where(w => w.TerminalNo.Equals(terminalList[k])).ToList().Count();
                    int? sum = daTxn.Where(w => w.TerminalNo.Equals(terminalList[k])).Sum(s => Int32.Parse(s.TransactionAmount));
                    //Total Amount
                    excelWorksheet.Cells[string.Format("J{0}:L{0}", temp)].Value = sum;
                    //Bill Ccy
                    excelWorksheet.Cells[string.Format("M{0}:N{0}", temp)].Value = "PHP";

                }


                excelPackage.Save();
                return new Tuple<MemoryStream, string>(ms, dupName);
            }
        }

        public async Task<Tuple<MemoryStream, string>> GenerateAtmRecon(List<vw_Transactions> daTxn)
        {

            string fName = string.Empty;
            //DeleteUsedTemplate("atmrecon"); 
            var dupName = await DuplicateTemplate("atmrecon");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Generated", dupName);
            FileInfo file = new FileInfo(filePath);
            MemoryStream ms = new MemoryStream();

            //FileInfo file = new FileInfo(fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {
                #region Variables

                ExcelWorkbook excelWorkBook = excelPackage.Workbook;

                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
                ExcelWorksheet terminalTemplateWS = excelWorkBook.Worksheets[1];

                int startIndex = 16;
                int addOnIndex_1 = 1, addOnIndex_2 = 2, addOnIndex_3 = 3;

                //===============================================================
                string h1_terminal = string.Format("B{0}:E{0}", startIndex);
                string h1_terminalValue = string.Format("G{0}:H{0}", startIndex);
                //===============================================================
                addOnIndex_1 += startIndex;
                string h2_switchReport = string.Format("B{0}:N{0}", addOnIndex_1);
                string h2_atmcReport = string.Format("O{0}:V{0}", addOnIndex_1);
                //===============================================================
                addOnIndex_2 += startIndex;
                string h3_sequenceNo = string.Format("B{0}", addOnIndex_2);
                string h3_transactionId = string.Format("C{0}:E{0}", addOnIndex_2);
                string h3_txnDateTime = string.Format("G{0}:I{0}", addOnIndex_2);
                string h3_txnAmount = string.Format("K{0}:M{0}", addOnIndex_2);
                string h3_cardSeqNumber = string.Format("N{0}", addOnIndex_2);
                string h3_systemTraceAuditNumber = string.Format("O{0}:P{0}", addOnIndex_2);
                string h3_settlementAmount = string.Format("R{0}:S{0}", addOnIndex_2);
                string h3_transType_2 = string.Format("T{0}:V{0}", addOnIndex_2);
                //===========================================================
                addOnIndex_3 += startIndex;
                string h4_sequenceNo = string.Format("B{0}", addOnIndex_3);
                string h4_transactionId = string.Format("C{0}:E{0}", addOnIndex_3);
                string h4_txnDateTime = string.Format("G{0}:I{0}", addOnIndex_3);
                string h4_txnAmount = string.Format("K{0}:M{0}", addOnIndex_3);
                string h4_cardSeqNumber = string.Format("N{0}", addOnIndex_3);
                string h4_systemTraceAuditNumber = string.Format("O{0}:P{0}", addOnIndex_3);
                string h4_settlementAmount = string.Format("R{0}:S{0}", addOnIndex_3);
                string h4_transType_2 = string.Format("T{0}:V{0}", addOnIndex_3);

                //============================================================
                #endregion

                var terminalList = daTxn.Select(s => s.TerminalNo).Distinct().ToList();
                for (int i = 0; i < terminalList.Count; i++)
                {
                    int first = 0;
                    int tmnlTxnCount = daTxn.Where(w => w.TerminalNo == terminalList[i]).ToList().Count();
                    for (int j = 0; j < daTxn.Count; j++)
                    {
                        int txnCount = 0;
                        if (terminalList[i] == daTxn[j].TerminalNo)
                        {
                            txnCount++;
                            addOnIndex_1 = 1; addOnIndex_2 = 2; addOnIndex_3 = 3;
                            if (i != 0 && first == 0)
                            {
                                startIndex = startIndex + 7;
                            }
                            if (first == 0)
                            {

                                #region Cell Index
                                #region h1
                                h1_terminal = string.Format("B{0}:E{0}", startIndex);
                                h1_terminalValue = string.Format("G{0}:H{0}", startIndex);
                                #endregion

                                #region h2
                                addOnIndex_1 += startIndex;
                                h2_switchReport = string.Format("B{0}:N{0}", addOnIndex_1);
                                h2_atmcReport = string.Format("O{0}:V{0}", addOnIndex_1);
                                #endregion

                                #region h3
                                addOnIndex_2 += startIndex;
                                h3_sequenceNo = string.Format("B{0}", addOnIndex_2);
                                h3_transactionId = string.Format("C{0}:E{0}", addOnIndex_2);
                                h3_txnDateTime = string.Format("G{0}:I{0}", addOnIndex_2);
                                h3_txnAmount = string.Format("K{0}:M{0}", addOnIndex_2);
                                h3_cardSeqNumber = string.Format("N{0}", addOnIndex_2);
                                h3_systemTraceAuditNumber = string.Format("O{0}:P{0}", addOnIndex_2);
                                h3_settlementAmount = string.Format("R{0}:S{0}", addOnIndex_2);
                                h3_transType_2 = string.Format("T{0}:V{0}", addOnIndex_2);
                                #endregion

                                #endregion


                                #region Insert Cell Formats
                                excelWorksheet.InsertRow(startIndex, 1, 7); //Terminal
                                excelWorksheet.InsertRow(startIndex + 1, 1, 8); //SWITCH REPORT / ATMC REPORT
                                excelWorksheet.InsertRow(startIndex + 2, 1, 9); //Columns header

                                #endregion
                                #region Merge
                                excelWorksheet.Cells[h1_terminal].Merge = true;
                                excelWorksheet.Cells[h1_terminalValue].Merge = true;
                                //============================================================
                                excelWorksheet.Cells[h2_switchReport].Merge = true;
                                excelWorksheet.Cells[h2_atmcReport].Merge = true;
                                //============================================================

                                excelWorksheet.Cells[h3_transactionId].Merge = true;
                                excelWorksheet.Cells[h3_txnDateTime].Merge = true;
                                excelWorksheet.Cells[h3_txnAmount].Merge = true;
                                excelWorksheet.Cells[h3_systemTraceAuditNumber].Merge = true;
                                excelWorksheet.Cells[h3_settlementAmount].Merge = true;
                                excelWorksheet.Cells[h3_transType_2].Merge = true;
                                //============================================================

                                excelWorksheet.Cells[h4_transactionId].Merge = true;
                                excelWorksheet.Cells[h4_txnDateTime].Merge = true;
                                excelWorksheet.Cells[h4_txnAmount].Merge = true;
                                excelWorksheet.Cells[h4_systemTraceAuditNumber].Merge = true;
                                excelWorksheet.Cells[h4_settlementAmount].Merge = true;
                                excelWorksheet.Cells[h4_transType_2].Merge = true;
                                //============================================================
                                #endregion

                                #region Label
                                excelWorksheet.Cells[h1_terminal].Value = "TERMINAL ID";
                                excelWorksheet.Cells[h1_terminalValue].Value = daTxn[j].TerminalNo;
                                //============================================================
                                excelWorksheet.Cells[h2_switchReport].Value = "SWITCH REPORT";
                                excelWorksheet.Cells[h2_atmcReport].Value = "ATMC REPORT";
                                //============================================================

                                excelWorksheet.Cells[h3_sequenceNo].Value = "S.No";
                                excelWorksheet.Cells[h3_txnDateTime].Value = "TXN DATE TIME";
                                excelWorksheet.Cells[h3_txnDateTime].Value = "Transaction Date Time";
                                excelWorksheet.Cells[h3_txnAmount].Value = "Transaction Amount";
                                excelWorksheet.Cells[h3_cardSeqNumber].Value = "Card Sequence Number";
                                excelWorksheet.Cells[h3_systemTraceAuditNumber].Value = "System Trace Audit Number";
                                excelWorksheet.Cells[h3_settlementAmount].Value = "Settlement Amount";
                                excelWorksheet.Cells[h3_transType_2].Value = "Transaction Type";

                                #endregion
                            }


                            if (tmnlTxnCount == txnCount)
                            {
                                //add summary template here

                            }
                            #region h4

                            addOnIndex_3 += startIndex;
                            h4_sequenceNo = string.Format("B{0}", addOnIndex_3);
                            h4_transactionId = string.Format("C{0}:E{0}", addOnIndex_3);
                            h4_txnDateTime = string.Format("G{0}:I{0}", addOnIndex_3);
                            h4_txnAmount = string.Format("K{0}:M{0}", addOnIndex_3);
                            h4_cardSeqNumber = string.Format("N{0}", addOnIndex_3);
                            h4_systemTraceAuditNumber = string.Format("O{0}:P{0}", addOnIndex_3);
                            h4_settlementAmount = string.Format("R{0}:S{0}", addOnIndex_3);
                            h4_transType_2 = string.Format("T{0}:V{0}", addOnIndex_3);
                            #endregion

                            #region Data
                            //============================================================
                            excelWorksheet.InsertRow(startIndex + 3, 1, 10); //Terminal label-header in-line

                            //content merge cells
                            excelWorksheet.Cells[h4_transactionId].Merge = true;
                            excelWorksheet.Cells[h4_txnDateTime].Merge = true;
                            excelWorksheet.Cells[h4_txnAmount].Merge = true;
                            excelWorksheet.Cells[h4_settlementAmount].Merge = true;
                            excelWorksheet.Cells[h4_systemTraceAuditNumber].Merge = true;
                            excelWorksheet.Cells[h4_transType_2].Merge = true;


                            excelWorksheet.Cells[h4_sequenceNo].Value = j + 1;
                            excelWorksheet.Cells[h4_transactionId].Value = daTxn[j].Stan;
                            excelWorksheet.Cells[h4_txnDateTime].Value = daTxn[j].TransactionDate;
                            excelWorksheet.Cells[h4_txnAmount].Value = daTxn[j].TransactionAmount;
                            excelWorksheet.Cells[h4_cardSeqNumber].Value = daTxn[j].CardSequenceNumber;
                            excelWorksheet.Cells[h4_systemTraceAuditNumber].Value = daTxn[j].SystemTraceAuditNumber;
                            excelWorksheet.Cells[h4_settlementAmount].Value = daTxn[j].SettlementAmount;
                            excelWorksheet.Cells[h4_transType_2].Value = daTxn[j].TransactionType;
                            //============================================================
                            #endregion

                            first++;
                            startIndex++;
                        }

                    }
                }

                excelPackage.Save();
                return new Tuple<MemoryStream, string>(ms, dupName);
            }
        }

        public async Task<Tuple<MemoryStream, string>> GenerateTotalTransactionAcrossTerminal(List<vw_Transactions> daTxn)
        {

            string fName = string.Empty;
            //DeleteUsedTemplate("atmrecon"); 
            var dupName = await DuplicateTemplate("txnterminal");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Generated", dupName);
            FileInfo file = new FileInfo(filePath);
            MemoryStream ms = new MemoryStream();

            //FileInfo file = new FileInfo(fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {

                #region Initialize
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;

                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
                ExcelWorksheet terminalTemplateWS = excelWorkBook.Worksheets[1];

                int startIndex = 22;
                string temp = string.Format("A{0}:E{0}", startIndex);

                excelWorksheet.Cells["AB2:AC2"].Value = DateTime.Now.ToString("dd-MM-yyyy");//report date | pending
                excelWorksheet.Cells["AB3:AC3"].Value = DateTime.Now.ToString("dd-MM-yyyy"); //created datae


                string h1_label_branch = string.Format("C{0}:D{0}", startIndex);
                string h1_content_branch = string.Format("F{0}:H{0}", startIndex);

                string lbl_SNo = string.Format("C{0}", startIndex + 1);
                string lbl_DateTime = string.Format("D{0}:H{0}", startIndex + 1);
                string lbl_PrimaryAccountNo = string.Format("I{0}:K{0}", startIndex + 1);
                string lbl_SysTraceNo = string.Format("L{0}:N{0}", startIndex + 1);
                string lbl_TxnAmount = string.Format("O{0}", startIndex + 1);
                string lbl_TxnCcy = string.Format("P{0}:Q{0}", startIndex + 1);
                string lbl_SettlementAmount = string.Format("R{0}", startIndex + 1);
                string lbl_SettlementCcy = string.Format("S{0}", startIndex + 1);
                string lbl_Fee = string.Format("T{0}", startIndex + 1);
                string lbl_MGR = string.Format("T{0}", startIndex + 1);
                string lbl_TxnType = string.Format("U{0}", startIndex + 1);
                string lbl_TerminalName = string.Format("V{0}:Z{0}", startIndex + 1);
                string lbl_CountryCode = string.Format("AA{0}:AB{0}", startIndex + 1);

                string con_SNo = string.Format("C{0}", startIndex + 2);
                string con_DateTime = string.Format("D{0}:H{0}", startIndex + 2);
                string con_PrimaryAccountNo = string.Format("I{0}:K{0}", startIndex + 2);
                string con_SysTraceNo = string.Format("L{0}:N{0}", startIndex + 2);
                string con_TxnAmount = string.Format("O{0}", startIndex + 2);
                string con_TxnCcy = string.Format("P{0}:Q{0}", startIndex + 2);
                string con_SettlementAmount = string.Format("R{0}", startIndex + 2);
                string con_SettlementCcy = string.Format("S{0}", startIndex + 2);
                string con_Fee = string.Format("T{0}", startIndex + 2);
                string con_TxnType = string.Format("U{0}", startIndex + 2);
                string con_TerminalName = string.Format("V{0}:Z{0}", startIndex + 2);
                string con_CountryCode = string.Format("AA{0}:AB{0}", startIndex + 2);

                #endregion

                var terminalList = daTxn.Select(s => s.TerminalNo).Distinct().ToList();
                for (int i = 0; i < terminalList.Count; i++)
                {
                    int first = 0;
                    int tmnlTxnCount = daTxn.Where(w => w.TerminalNo == terminalList[i]).ToList().Count();
                    for (int j = 0; j < daTxn.Count; j++)
                    {
                        int txnCount = 0;
                        if (terminalList[i] == daTxn[j].TerminalNo)
                        {
                            txnCount++;

                            if (i != 0 && first == 0)
                            {
                                startIndex = startIndex + 4;
                            }
                            if (first == 0)
                            {


                                h1_label_branch = string.Format("C{0}:D{0}", startIndex);
                                h1_content_branch = string.Format("F{0}:H{0}", startIndex);

                                lbl_SNo = string.Format("C{0}", startIndex + 1);
                                lbl_DateTime = string.Format("D{0}:H{0}", startIndex + 1);
                                lbl_PrimaryAccountNo = string.Format("I{0}:K{0}", startIndex + 1);
                                lbl_SysTraceNo = string.Format("L{0}:N{0}", startIndex + 1);
                                lbl_TxnAmount = string.Format("O{0}", startIndex + 1);
                                lbl_TxnCcy = string.Format("P{0}:Q{0}", startIndex + 1);
                                lbl_SettlementAmount = string.Format("R{0}", startIndex + 1);
                                lbl_SettlementCcy = string.Format("S{0}", startIndex + 1);
                                lbl_Fee = string.Format("T{0}", startIndex + 1);
                                lbl_MGR = string.Format("T{0}", startIndex + 1);
                                lbl_TxnType = string.Format("U{0}", startIndex + 1);
                                lbl_TerminalName = string.Format("V{0}:Z{0}", startIndex + 1);
                                lbl_CountryCode = string.Format("AA{0}:AB{0}", startIndex + 1);

                                excelWorksheet.InsertRow(startIndex, 1, 13); //Branch
                                excelWorksheet.InsertRow(startIndex + 1, 1, 14); //label





                                excelWorksheet.Cells[h1_label_branch].Merge = true;
                                excelWorksheet.Cells[h1_content_branch].Merge = true;
                                //============================================================


                                excelWorksheet.Cells[lbl_DateTime].Merge = true;
                                excelWorksheet.Cells[lbl_PrimaryAccountNo].Merge = true;
                                excelWorksheet.Cells[lbl_SysTraceNo].Merge = true;
                                excelWorksheet.Cells[lbl_TxnCcy].Merge = true;
                                excelWorksheet.Cells[lbl_SettlementAmount].Merge = true;
                                excelWorksheet.Cells[lbl_TerminalName].Merge = true;
                                excelWorksheet.Cells[lbl_CountryCode].Merge = true;

                                //============================================================


                                excelWorksheet.Cells[h1_label_branch].Value = "BRANCH :";
                                excelWorksheet.Cells[h1_content_branch].Value = "TEST";
                                //============================================================

                                excelWorksheet.Cells[lbl_SNo].Value = "S.No";
                                excelWorksheet.Cells[lbl_DateTime].Value = "Date & Time";
                                excelWorksheet.Cells[lbl_PrimaryAccountNo].Value = "Primary Account Number.";
                                excelWorksheet.Cells[lbl_SysTraceNo].Value = "System Trace No.";
                                excelWorksheet.Cells[lbl_TxnAmount].Value = "Transaction  Amount";
                                excelWorksheet.Cells[lbl_TxnCcy].Value = "Transaction Ccy";
                                excelWorksheet.Cells[lbl_SettlementAmount].Value = "Settlement Amount";
                                excelWorksheet.Cells[lbl_SettlementCcy].Value = "Settlement Ccy";
                                excelWorksheet.Cells[lbl_Fee].Value = "Fee";
                                excelWorksheet.Cells[lbl_MGR].Value = "MOBI Fee";
                                excelWorksheet.Cells[lbl_TxnType].Value = "Transaction Type";
                                excelWorksheet.Cells[lbl_TerminalName].Value = "Terminal Name";
                                excelWorksheet.Cells[lbl_CountryCode].Value = "Country Code";
                                //============================================================


                            }


                            if (tmnlTxnCount == txnCount)
                            {
                                //add summary template here

                            }

                            con_SNo = string.Format("C{0}", startIndex + 2);
                            con_DateTime = string.Format("D{0}:H{0}", startIndex + 2);
                            con_PrimaryAccountNo = string.Format("I{0}:K{0}", startIndex + 2);
                            con_SysTraceNo = string.Format("L{0}:N{0}", startIndex + 2);
                            con_TxnAmount = string.Format("O{0}", startIndex + 2);
                            con_TxnCcy = string.Format("P{0}:Q{0}", startIndex + 2);
                            con_SettlementAmount = string.Format("R{0}", startIndex + 2);
                            con_SettlementCcy = string.Format("S{0}", startIndex + 2);
                            con_Fee = string.Format("T{0}", startIndex + 2);
                            con_TxnType = string.Format("U{0}", startIndex + 2);
                            con_TerminalName = string.Format("V{0}:Z{0}", startIndex + 2);
                            con_CountryCode = string.Format("AA{0}:AB{0}", startIndex + 2);



                            //============================================================
                            excelWorksheet.InsertRow(startIndex + 2, 1, 15); //content
                            //content merge cells
                            excelWorksheet.Cells[con_DateTime].Merge = true;
                            excelWorksheet.Cells[con_PrimaryAccountNo].Merge = true;
                            excelWorksheet.Cells[con_SysTraceNo].Merge = true;
                            excelWorksheet.Cells[con_TxnCcy].Merge = true;
                            excelWorksheet.Cells[con_TerminalName].Merge = true;
                            excelWorksheet.Cells[con_CountryCode].Merge = true;
                            //============================================================
                            excelWorksheet.Cells[con_SNo].Value = j + 1;
                            excelWorksheet.Cells[con_DateTime].Value = daTxn[j].TransactionDate;
                            excelWorksheet.Cells[con_PrimaryAccountNo].Value = daTxn[j].PrimaryAccountNumber;
                            excelWorksheet.Cells[con_SysTraceNo].Value = daTxn[j].SystemTraceAuditNumber;
                            excelWorksheet.Cells[con_TxnAmount].Value = daTxn[j].TransactionAmount;
                            excelWorksheet.Cells[con_TxnCcy].Value = "PHP";
                            excelWorksheet.Cells[con_SettlementAmount].Value = daTxn[j].SettlementAmount;
                            excelWorksheet.Cells[con_SettlementCcy].Value = "PHP";
                            excelWorksheet.Cells[con_Fee].Value = daTxn[j].ConvenienceFee;
                            excelWorksheet.Cells[con_TxnType].Value = daTxn[j].TransactionType;
                            excelWorksheet.Cells[con_TerminalName].Value = "Test";//This date will be coming from TerminalInfo
                            excelWorksheet.Cells[con_CountryCode].Value = "PH";

                            first++;
                            startIndex++;
                        }

                    }
                }

                //=================== Pending summary
                //for (int k = 0; k < terminalList.Count; k++)
                //{
                //    if (k == 0)
                //    {
                //        //copy cell format from row 15
                //        excelWorksheet.InsertRow(startIndex + 3, 1, 17); //label summary
                //        excelWorksheet.InsertRow(startIndex + 4, 1, 18); //label

                //    }



                //}
                //======================
                excelPackage.Save();
                return new Tuple<MemoryStream, string>(ms, dupName);
            }

        }

        public async Task<Tuple<MemoryStream, string>> GenerateTransactionFee(List<vw_Transactions> daTxn)
        {

            string fName = string.Empty;
            //DeleteUsedTemplate("atmrecon"); 
            var dupName = await DuplicateTemplate("txnfee");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Generated", dupName);
            FileInfo file = new FileInfo(filePath);
            MemoryStream ms = new MemoryStream();

            //FileInfo file = new FileInfo(fileName);
            using (ExcelPackage excelPackage = new ExcelPackage(file))
            {

                #region Initialize
                ExcelWorkbook excelWorkBook = excelPackage.Workbook;

                ExcelWorksheet excelWorksheet = excelWorkBook.Worksheets.First();
                ExcelWorksheet terminalTemplateWS = excelWorkBook.Worksheets[1];

                int startIndex = 22;
                string temp = string.Format("A{0}:E{0}", startIndex);

                excelWorksheet.Cells["AA2"].Value = DateTime.Now.ToString("MM-dd-yyyy");//report date | pending
                excelWorksheet.Cells["AA3"].Value = DateTime.Now.ToString("MM-dd-yyyy"); //created datae


                string h1_label_branch = string.Format("C{0}:D{0}", startIndex);
                string h1_content_branch = string.Format("F{0}:H{0}", startIndex);

                string lbl_SNo = string.Format("C{0}", startIndex + 1);
                string lbl_DateTime = string.Format("D{0}:H{0}", startIndex + 1);
                string lbl_PrimaryAccountNo = string.Format("I{0}:K{0}", startIndex + 1);
                string lbl_SysTraceNo = string.Format("L{0}:N{0}", startIndex + 1);
                string lbl_TxnAmount = string.Format("O{0}", startIndex + 1);
                string lbl_TxnCcy = string.Format("P{0}:Q{0}", startIndex + 1);
                string lbl_SettlementAmount = string.Format("R{0}", startIndex + 1);
                string lbl_SettlementCcy = string.Format("S{0}", startIndex + 1);
                string lbl_Fee = string.Format("T{0}", startIndex + 1);
                string lbl_MGR = string.Format("U{0}", startIndex + 1);
                string lbl_TxnTypeE = string.Format("V{0}", startIndex + 1);
                string lbl_TerminalNameE = string.Format("W{0}:AA{0}", startIndex + 1);
                string lbl_CountryCodeE = string.Format("AB{0}:AC{0}", startIndex + 1);

                string con_SNo = string.Format("C{0}", startIndex + 2);
                string con_DateTime = string.Format("D{0}:H{0}", startIndex + 2);
                string con_PrimaryAccountNo = string.Format("I{0}:K{0}", startIndex + 2);
                string con_SysTraceNo = string.Format("L{0}:N{0}", startIndex + 2);
                string con_TxnAmount = string.Format("O{0}", startIndex + 2);
                string con_TxnCcy = string.Format("P{0}:Q{0}", startIndex + 2);
                string con_SettlementAmount = string.Format("R{0}", startIndex + 2);
                string con_SettlementCcy = string.Format("S{0}", startIndex + 2);
                string con_Fee = string.Format("T{0}", startIndex + 2);
                string con_MGR = string.Format("U{0}", startIndex + 2);
                string con_TxnTypeE = string.Format("V{0}", startIndex + 2);
                string con_TerminalNameE = string.Format("W{0}:AA{0}", startIndex + 2);
                string con_CountryCodeE = string.Format("AB{0}:AC{0}", startIndex + 2);

                #endregion

                var terminalList = daTxn.Select(s => s.TerminalNo).Distinct().ToList();
                for (int i = 0; i < terminalList.Count; i++)
                {
                    int first = 0;
                    int totalMobiFee = 0;
                    int totalFee = 0;
                    int tmnlTxnCount = daTxn.Where(w => w.TerminalNo == terminalList[i]).ToList().Count();
                    for (int j = 0; j < daTxn.Count; j++)
                    {
                        int txnCount = 0;
                        if (terminalList[i] == daTxn[j].TerminalNo)
                        {
                            txnCount++;

                            if (i != 0 && first == 0)
                            {
                                startIndex = startIndex + 4;
                            }
                            if (first == 0)
                            {


                                h1_label_branch = string.Format("C{0}:D{0}", startIndex);
                                h1_content_branch = string.Format("F{0}:H{0}", startIndex);


                                lbl_SNo = string.Format("C{0}", startIndex + 1);
                                lbl_DateTime = string.Format("D{0}:H{0}", startIndex + 1);
                                lbl_PrimaryAccountNo = string.Format("I{0}:K{0}", startIndex + 1);
                                lbl_SysTraceNo = string.Format("L{0}:N{0}", startIndex + 1);
                                lbl_TxnAmount = string.Format("O{0}", startIndex + 1);
                                lbl_TxnCcy = string.Format("P{0}:Q{0}", startIndex + 1);
                                lbl_SettlementAmount = string.Format("R{0}", startIndex + 1);
                                lbl_SettlementCcy = string.Format("S{0}", startIndex + 1);
                                lbl_Fee = string.Format("T{0}", startIndex + 1);
                                lbl_MGR = string.Format("U{0}", startIndex + 1);
                                lbl_TxnTypeE = string.Format("V{0}", startIndex + 1);
                                lbl_TerminalNameE = string.Format("W{0}:AA{0}", startIndex + 1);
                                lbl_CountryCodeE = string.Format("AB{0}:AC{0}", startIndex + 1);

                                excelWorksheet.InsertRow(startIndex, 1, 13); //Branch
                                excelWorksheet.InsertRow(startIndex + 1, 1, 14); //label





                                excelWorksheet.Cells[h1_label_branch].Merge = true;
                                excelWorksheet.Cells[h1_content_branch].Merge = true;
                                //============================================================

                                excelWorksheet.Cells[lbl_DateTime].Merge = true;
                                excelWorksheet.Cells[lbl_PrimaryAccountNo].Merge = true;
                                excelWorksheet.Cells[lbl_SysTraceNo].Merge = true;
                                excelWorksheet.Cells[lbl_TxnCcy].Merge = true;
                                excelWorksheet.Cells[lbl_SettlementAmount].Merge = true;
                                excelWorksheet.Cells[lbl_TerminalNameE].Merge = true;
                                excelWorksheet.Cells[lbl_CountryCodeE].Merge = true;

                                //============================================================


                                excelWorksheet.Cells[h1_label_branch].Value = "BRANCH :";
                                excelWorksheet.Cells[h1_content_branch].Value = daTxn[j].City;
                                //============================================================


                                excelWorksheet.Cells[lbl_SNo].Value = "S.No";
                                excelWorksheet.Cells[lbl_DateTime].Value = "Date & Time";
                                excelWorksheet.Cells[lbl_PrimaryAccountNo].Value = "Primary Account Number.";
                                excelWorksheet.Cells[lbl_SysTraceNo].Value = "System Trace No.";
                                excelWorksheet.Cells[lbl_TxnAmount].Value = "Transaction  Amount";
                                excelWorksheet.Cells[lbl_TxnCcy].Value = "Transaction Ccy";
                                excelWorksheet.Cells[lbl_SettlementAmount].Value = "Settlement Amount";
                                excelWorksheet.Cells[lbl_SettlementCcy].Value = "Settlement Ccy";
                                excelWorksheet.Cells[lbl_Fee].Value = "Fee";
                                excelWorksheet.Cells[lbl_MGR].Value = "MOBI Fee";
                                excelWorksheet.Cells[lbl_TxnTypeE].Value = "Transaction Type";
                                excelWorksheet.Cells[lbl_TerminalNameE].Value = "Terminal Name";
                                excelWorksheet.Cells[lbl_CountryCodeE].Value = "Country Code";
                                //============================================================


                            }


                            if (tmnlTxnCount == txnCount)
                            {
                                //add summary template here

                            }

                            con_SNo = string.Format("C{0}", startIndex + 2);
                            con_DateTime = string.Format("D{0}:H{0}", startIndex + 2);
                            con_PrimaryAccountNo = string.Format("I{0}:K{0}", startIndex + 2);
                            con_SysTraceNo = string.Format("L{0}:N{0}", startIndex + 2);
                            con_TxnAmount = string.Format("O{0}", startIndex + 2);
                            con_TxnCcy = string.Format("P{0}:Q{0}", startIndex + 2);
                            con_SettlementAmount = string.Format("R{0}", startIndex + 2);
                            con_SettlementCcy = string.Format("S{0}", startIndex + 2);
                            con_Fee = string.Format("T{0}", startIndex + 2);
                            con_MGR = string.Format("U{0}", startIndex + 2);
                            con_TxnTypeE = string.Format("V{0}", startIndex + 2);
                            con_TerminalNameE = string.Format("W{0}:AA{0}", startIndex + 2);
                            con_CountryCodeE = string.Format("AB{0}:AC{0}", startIndex + 2);


                            //============================================================
                            excelWorksheet.InsertRow(startIndex + 2, 1, 15); //content
                            //content merge cells
                            excelWorksheet.Cells[con_DateTime].Merge = true;
                            excelWorksheet.Cells[con_PrimaryAccountNo].Merge = true;
                            excelWorksheet.Cells[con_SysTraceNo].Merge = true;
                            excelWorksheet.Cells[con_TxnCcy].Merge = true;
                            excelWorksheet.Cells[con_TerminalNameE].Merge = true;
                            excelWorksheet.Cells[con_CountryCodeE].Merge = true;
                            //============================================================
                            excelWorksheet.Cells[con_SNo].Value = j + 1;
                            excelWorksheet.Cells[con_DateTime].Value = daTxn[j].TransactionDate;
                            excelWorksheet.Cells[con_PrimaryAccountNo].Value = daTxn[j].PrimaryAccountNumber;
                            excelWorksheet.Cells[con_SysTraceNo].Value = daTxn[j].SystemTraceAuditNumber;
                            excelWorksheet.Cells[con_TxnAmount].Value = daTxn[j].TransactionAmount;
                            excelWorksheet.Cells[con_TxnCcy].Value = "PHP";
                            int setAmount = int.Parse(daTxn[j].SettlementAmount) - int.Parse(daTxn[j].ConvenienceFee);
                            excelWorksheet.Cells[con_SettlementAmount].Value = setAmount;
                            excelWorksheet.Cells[con_SettlementCcy].Value = "PHP";
                            totalFee += int.Parse(daTxn[j].ConvenienceFee);
                            excelWorksheet.Cells[con_Fee].Value = daTxn[j].ConvenienceFee;
                            var tempa = int.Parse(daTxn[j].ConvenienceFee);
                            totalMobiFee += tempa / 2;
                            excelWorksheet.Cells[con_MGR].Value = tempa / 2;//computation here
                            excelWorksheet.Cells[con_TxnTypeE].Value = daTxn[j].TransactionType;
                            excelWorksheet.Cells[con_TerminalNameE].Value = "Test";//This date will be coming from TerminalInfo
                            excelWorksheet.Cells[con_CountryCodeE].Value = "PH";

                            first++;
                            startIndex++;
                        }

                    }//loop
                    excelWorksheet.InsertRow(startIndex + 2, 1, 21); //content
                    excelWorksheet.Cells[string.Format("T{0}", startIndex + 2)].Value = totalFee;//computation heretotalMobiFee
                    excelWorksheet.Cells[string.Format("U{0}", startIndex + 2)].Value = totalMobiFee;
                }//loop

                excelPackage.Save();
                return new Tuple<MemoryStream, string>(ms, dupName);
            }

        }


        private async Task<string> DuplicateTemplate(string template)
        {
            var dateTime = DateTime.Now.ToString("yyyyMMddhhss");
            string docName = GetTemplateName(template);//original file
            var duplicateName = string.Format(@"{0}_{1}.xlsx", docName, dateTime);
            var destinationFile = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Generated", duplicateName);

            var sourceFile = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Templates", string.Format("{0}Duplicate.xlsx", docName));

            try
            {
                await Task.Run(() =>
                {
                    File.Copy(sourceFile, destinationFile, true);
                });
            }
            catch (IOException iox)
            {
                var temp = iox;
            }
            return duplicateName;
        }
        private void DeleteUsedTemplate(string template)
        {
            try
            {
                string docName = GetTemplateName(template);
                var sourceFile = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Generated", string.Format(@"{0}.xlsx", docName));

                if (File.Exists(sourceFile))
                {
                    System.IO.File.Delete(sourceFile);
                }

            }
            catch (Exception ex)
            {
                var temp = ex;
                throw;
            }

        }

        private string GetTemplateName(string name)
        {
            string tempName = string.Empty;
            switch (name)
            {
                case "acquirer":
                    tempName = "DailyAcquirer";
                    break;
                case "atmrecon":
                    tempName = "DailyATMRecon";
                    break;
                case "txnterminal":
                    tempName = "TotalTransactionsAcrossTerminal";
                    break;
                case "txnfee":
                    tempName = "TransactionFee";
                    break;
                default:
                    tempName = string.Empty;
                    break;
            }
            return tempName;
        }



    }
}