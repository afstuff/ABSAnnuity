using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Web.Security;
using System.Security.Cryptography;
using System.Globalization;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Common;
using System.Web;
//using System.Web.Configuration;



namespace CustodianAnnuity.Data
{
    public class hashHelper
    {
        public static void InsertAcctChart(string compCode,
                                         string MainAcctCode,
                                         string SubAcctCode,
                                         string MainDesc,
                                         string SubDesc,
                                         string AcctLevel,
                                         string MainGrp,
                                         string LedgerCode,
                                         string LedgerType,
                                         string Sub1Grp,
                                          string Sub2Grp,
                                         string ProductCode,
                                         string Bus_Type,
                                         string PolyType,
                                         string AcctStatus,
                                         string Mode,
                                         string Flag,
                                         DateTime KeyDate,
                                         string OperId, string ConnString)
        {


            OleDbConnection conn;
            conn = new OleDbConnection(ConnString);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SPGL_FIN_ACCT_CHART_INSERT";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@pTBFN_ACCT_COMP_CD", compCode);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_MAIN_CD", MainAcctCode);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_SUB_CD", SubAcctCode);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_MAIN_DESC", MainDesc);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_SUB_DESC", SubDesc);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_LEVEL", AcctLevel);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_MAIN_GRP", MainGrp);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_LEDG_CODE", LedgerCode);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_LEDG_TYPE", LedgerType);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_SUB1_GRP", Sub1Grp);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_SUB2_GRP", Sub2Grp);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_PRDCT_CD", ProductCode);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_BUS_TYPE", Bus_Type);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_POL_TYPE", PolyType);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_STATUS", AcctStatus);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_MODE", Mode);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_FLAG", Flag);
            cmd.Parameters.AddWithValue("@pTBFN_ACCT_KEYDTE", KeyDate);
            cmd.Parameters.AddWithValue("@TBFN_ACCT_OPERID", OperId);
            try
            {
                conn.Open();
                //int  saved  = CType(cmd.ExecuteScalar(), Short)
                cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {

            }

        }

        public static string GetMainAcctCode(String CustCatCode, string mystrConn)
        {
            string MainActtCode = "";
            string sqlStr = "select * from TBIL_CUST_CAT WHERE TBIL_CUST_CATEG ='" + CustCatCode + "'";
            OleDbConnection conn;
            conn = new OleDbConnection(mystrConn);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlStr;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MainActtCode = dr["TBIL_CUST_CAT_CNTRL_ACCT"].ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return MainActtCode;
        }

        public static string GetMainAccountCode(String CustCatCode, string mystrConn)
        {
            string MainActtCode = "";
            string sqlStr = "select * from TBIL_INS_CLASS WHERE TBIL_INS_CLASS_CD ='" + CustCatCode + "'";
            OleDbConnection conn;
            conn = new OleDbConnection(mystrConn);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = sqlStr;
            cmd.CommandType = CommandType.Text;
            OleDbDataReader dr;
            try
            {
                conn.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MainActtCode = dr["TBIL_INS_CLASS_SHRT_DESC"].ToString();
                }
                conn.Close();
            }
            catch (Exception ex)
            {

            }
            return MainActtCode;
        }
    }
}
