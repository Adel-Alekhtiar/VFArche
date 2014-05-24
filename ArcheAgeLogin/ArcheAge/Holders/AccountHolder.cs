﻿using ArcheAgeLogin.ArcheAge.Structuring;
using ArcheAgeLogin.Properties;
using LocalCommons.Native.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcheAgeLogin.ArcheAge.Holders
{
    public class AccountHolder
    {
        private static List<Account> m_DbAccounts;

        /// <summary>
        /// Loaded List of Accounts.
        /// </summary>
        public static List<Account> AccountList
        {
            get { return m_DbAccounts; }
        }

        /// <summary>
        /// Gets Account By Name With LINQ Or Return Null.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Account GetAccount(string name)
        {
            return m_DbAccounts.FirstOrDefault(acc => acc.Name == name);
        }

        /// <summary>
        /// Fully Load Account Data From Current MySQL Db.
        /// </summary>
        public static void LoadAccountData()
        {
            m_DbAccounts = new List<Account>();
            MySqlConnection con = new MySqlConnection(Settings.Default.DataBaseConnectionString);
            try
            {
                con.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM `accounts`", con);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Account account = new Account();
                    account.AccessLevel = (byte)reader.GetInt32("mainaccess");
                    account.AccountId = reader.GetInt32("id");
                    account.Name = reader.GetString("name");
                    account.Password = reader.GetString("password");
                    account.LastEnteredTime = reader.GetInt64("last_online");
                    account.LastIp = reader.GetString("last_ip");
                    account.Membership = (byte)reader.GetInt32("useraccess");
                    account.Characters = reader.GetInt32("characters");
                    m_DbAccounts.Add(account);
                }
                command = null;
                reader = null;
            }
            finally
            {
                con.Close();
                con = null;
            }

            Logger.Trace("Loaded {0} Accounts", m_DbAccounts.Count);
        }
       
        /// <summary>
        /// Inserts Or Update Existing Account Into your current Login Server MySql DataBase.
        /// </summary>
        /// <param name="account">Your Account Which you want Insert(If Not Exist) Or Update(If Exist)</param>
        public static void InsertOrUpdate(Account account)
        {
            MySqlConnection con = new MySqlConnection(Settings.Default.DataBaseConnectionString);
            try
            {
                con.Open();
                MySqlCommand command = null;
                if (m_DbAccounts.Contains(account))
                {
                    command = new MySqlCommand("UPDATE `accounts` SET `id` = @id, `name` = @name, `mainaccess` = @mainaccess, `useraccess` = @useraccess, `last_ip` = @lastip, `password` = @password, `last_online` = @lastonline, `characters` = @characters WHERE `id` = @aid", con);
                }
                else
                {
                    command = new MySqlCommand("INSERT INTO `accounts`(id, name, mainaccess, useraccess, last_ip, password, last_online, characters) VALUES(@id, @name, @mainaccess, @useraccess, @lastip, @password, @lastonline, @characters)", con);
                }

                MySqlParameterCollection parameters = command.Parameters;
                parameters.Add("@id", MySqlDbType.Int32).Value = account.AccountId;
                parameters.Add("@name", MySqlDbType.String).Value = account.Name;
                parameters.Add("@mainaccess", MySqlDbType.Byte).Value = account.AccessLevel;
                parameters.Add("@useraccess", MySqlDbType.Byte).Value = account.Membership;
                parameters.Add("@lastip", MySqlDbType.String).Value = account.LastIp;
                parameters.Add("@password", MySqlDbType.String).Value = account.Password;
                parameters.Add("@lastonline", MySqlDbType.Int64).Value = account.LastEnteredTime;
                if (m_DbAccounts.Contains(account))
                    parameters.Add("@aid", MySqlDbType.Int32).Value = account.AccountId;

                parameters.Add("@characters", MySqlDbType.Int32).Value = account.Characters;

                command.ExecuteNonQuery();
                command = null;
            }
            finally
            {
                m_DbAccounts.Add(account);
                con.Close();
                con = null;
            }
        }
    }
}
