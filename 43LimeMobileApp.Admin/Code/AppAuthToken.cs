/*************************************************************************
 * Author: DCoreyDuke
 ************************************************************************/
using _43LimeMobileApp.Admin.Services;
using _43LimeMobileApp.Models.Entities;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace _43LimeMobileApp.Admin
{
	public enum AppAuthTokenType
	{
		Empty = 1,
		Authenticated = 2,
		Canceled = 3
	}


	/// <summary>
	/// Application Authorization Token
	/// </summary>
	public class AppAuthToken
	{	    
		private readonly string connectionString = new SqliteConnectionStringBuilder(){
			DataSource = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/App_Data"), "data.db")
			}.ConnectionString;

		private UserDataService _userDataService;
		private RoleDataService _roleDataService;

		public int Id{get;set;}
		public User User{get;set;}
		public Role Role{get;set;}
		public DateTime LoginTime{get;set;}
		public DateTime LogoutTime{get;set;}
		public AppAuthTokenType TokenType{get;set;} // Empty, Authenticated, or Canceled

		public AppAuthToken()
		{		
			_userDataService = new UserDataService();
			_roleDataService = new RoleDataService();			
		}


		/// <summary>
		/// Creates the Empty AppAuth Token for the Session
		/// The Token Is Created with Empty/Default Values
		/// </summary>
		public AppAuthToken CreateEmptyToken()
		{
			AppAuthToken token = new AppAuthToken();
			token.User = null;
			token.Role = null;
			token.LoginTime = new DateTime();
			token.LogoutTime = new DateTime();
			token.Id = GetAppAuthTokenCount() + 1 + new Random().Next(1, 99);
			token.TokenType = AppAuthTokenType.Empty;
			CreateEmptyAppAuthToken(token.Id);
			return token;
		}

		/// <summary>
		/// Creates the authenticated user application authentication token.
		/// This Can Only Be Returned After A Successful Login
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <param name="loginTime">The login time.</param>
		public AppAuthToken CreateAuthenticatedUserToken(AppAuthToken emptyToken, string userId, DateTime loginTime)
		{
			// At This Point We Assume the UserId is Valid
			AppAuthToken token = new AppAuthToken();
			token.User = _userDataService.Get(userId);
			token.Role = _roleDataService.Get(this.User.RoleId);
			token.LoginTime = loginTime;
			token.LogoutTime = DateTime.MinValue;
			token.Id = emptyToken.Id; //Id From Old Token
			token.TokenType = AppAuthTokenType.Authenticated;

			Clear(userId); // Clears any existing AppAuth Tokens For This User

			UpdateAppAuthToken(token); // Update The Database Record

			return token;
		}

		/// <summary>
		/// Cancels the authenticated user token.
		/// This is Done after a Logout, Session End, or ApplicationEnd
		/// </summary>
		/// <param name="authenticatedUserToken">The authenticated user token.</param>
		public AppAuthToken CancelAuthenticatedUserToken(AppAuthToken authenticatedUserToken)
		{
			AppAuthToken token = new AppAuthToken();
			token.User = authenticatedUserToken.User;
			token.Role = authenticatedUserToken.Role;
			token.LoginTime = authenticatedUserToken.LoginTime;
			token.LogoutTime = DateTime.Now;
			token.Id = authenticatedUserToken.Id;
			token.TokenType = AppAuthTokenType.Canceled;

			UpdateAppAuthToken(token);

			return token;
		}

		/// <summary>
		/// Gets the current application authorization token.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		public AppAuthToken GetCurrentUserToken(string userId)
		{
			return GetAppAuthToken(userId);
		}


		/// <summary>
		/// Clears any existing AppAuth Records for the user
		/// </summary>
		private void Clear(string userId)
		{
			int i = GetAppAuthTokenCount();
			if(i > 0)
			{
				List<AppAuthToken> _l = new List<AppAuthToken>(i);
				for(int ii = 0; ii <= i; ii++)
				{
					_l[i] = GetAppAuthToken(userId);
				}
				foreach(AppAuthToken a in _l)
				{
					DeleteAppAuthToken(a.Id);
				}
				
			}
		}		

		/// <summary>
		/// Creates the AppAuthToken Record.
		/// Creates the Record with default/empty values
		/// </summary>
		private void CreateEmptyAppAuthToken(int id)
		{         
			using(var con = new SqliteConnection(connectionString))
			{
				try
				{                   
					using(var trans = con.BeginTransaction())
					{
						var ins = con.CreateCommand();
						ins.Transaction = trans;
						ins.CommandText = @"INSERT INTO AppAuth(Id, UserId, UserName, RoleId, RoleName, IsActive, IsOnline, LoginTime, LogoutTime, TokenType)
											VALUES($Id, $UserId, $UserName, $RoleId, $RoleName, $IsActive, $IsOnline, $LoginTime, $LogoutTime, $TokenType)";
						ins.Parameters.AddWithValue("$Id", id);
						ins.Parameters.AddWithValue("$UserId", string.Empty);
						ins.Parameters.AddWithValue("$UserName", string.Empty);
						ins.Parameters.AddWithValue("$RoleId", -1);
						ins.Parameters.AddWithValue("$RoleName", string.Empty);
						ins.Parameters.AddWithValue("$IsActive", -1);
						ins.Parameters.AddWithValue("$IsOnline", -1);
						ins.Parameters.AddWithValue("$LoginTime", string.Empty);
						ins.Parameters.AddWithValue("$LogoutTime", string.Empty);
						ins.Parameters.AddWithValue("$TokenType", 1);

						con.Open();
						ins.ExecuteNonQuery();
						trans.Commit();
					}
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			return;
		}
		
		/// <summary>
		/// Updates and returns the application authentication token.
		/// </summary>
		private  AppAuthToken UpdateAppAuthToken(AppAuthToken token)
		{         
			using(var con = new SqliteConnection(connectionString))
			{
				try
				{                   
					using(var trans = con.BeginTransaction())
					{
						var upd = con.CreateCommand();
						upd.Transaction = trans;
						upd.CommandText = @"UPDATE AppAuth
											SET UserId		= $UserId,
												UserName	= $UserName,
												RoleId		= $RoleId,
												RoleName	= $RoleName,
												IsActive	= $IsActive,
												IsOnline	= $IsOnline,
												LoginTime	= $LoginTime,
												LogoutTime	= $LogoutTime,
												TokenType   = $TokenType
											WHERE
												Id = $Id";
						upd.Parameters.AddWithValue("$Id", token.Id);
						upd.Parameters.AddWithValue("$UserId", token.User.UserId.ToString());
						upd.Parameters.AddWithValue("$UserName", token.User.Username.ToString());
						upd.Parameters.AddWithValue("$RoleId", token.Role.Id);
						upd.Parameters.AddWithValue("$RoleName", token.Role.RoleName.ToString());
						upd.Parameters.AddWithValue("$IsActive", 1);
						upd.Parameters.AddWithValue("$IsOnline", 1);
						upd.Parameters.AddWithValue("$LoginTime", token.LoginTime.ToString());
						upd.Parameters.AddWithValue("$LogoutTime", token.LoginTime.ToString());
						upd.Parameters.AddWithValue("$TokenType", token.TokenType);

						con.Open();
						upd.ExecuteNonQuery();
						trans.Commit();
					}
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			return token;
		}

		/// <summary>
		/// Gets the application authentication token.
		/// </summary>
		private AppAuthToken GetAppAuthToken(string userId)
		{       
			AppAuthToken token = new AppAuthToken();

			using(var con = new SqliteConnection(connectionString))
			{
				try
				{                   
					using(var trans = con.BeginTransaction())
					{
						var sel = con.CreateCommand();
						sel.Transaction = trans;
						sel.CommandText = @"SELECT Id, UserId, UserName, RoleId, RoleName, IsActive, IsOnline, LoginTime, LogoutTime, TokenType
											FROM AppAuth
											WHERE UserId = $UserId";
						sel.Parameters.AddWithValue("$UserId", userId);						

						con.Open();
						
						using(var rdr = sel.ExecuteReader())
						{
							if(rdr.HasRows)
							{
								while(rdr.Read())
								{
									User user = new User
									(
										rdr["UserId"].ToString(), 
										rdr["UserName"].ToString(), 
										int.Parse(rdr["RoleId"].ToString()), 
										bool.Parse(rdr["IsActive"].ToString()), 
										bool.Parse(rdr["IsOnline"].ToString())
									);
									Role role = new Role(int.Parse(rdr["RoleId"].ToString()),rdr["RoleName"].ToString());

									token.Id = int.Parse(rdr["Id"].ToString());
									token.User = user;
									token.Role = role;
									token.LoginTime = DateTime.Parse(rdr["LoginTime"].ToString());
									token.LogoutTime = DateTime.Parse(rdr["LogoutTime"].ToString());
									token.TokenType = (AppAuthTokenType)int.Parse(rdr["TokenType"].ToString());
								}
							}
						}


						trans.Commit();
					}
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			return token;
		}

		/// <summary>
		/// Gets the application authentication token count
		/// </summary>
		private int GetAppAuthTokenCount()
		{       
			int rowCount = 0;
			using(var con = new SqliteConnection(connectionString))
			{
				try
				{                   
					using(var trans = con.BeginTransaction())
					{
						var cnt = con.CreateCommand();
						cnt.Transaction = trans;
						cnt.CommandType = System.Data.CommandType.Text;
						cnt.CommandText = @"SELECT COUNT(*)
											FROM AppAuth";
						
						con.Open();
						
						rowCount = Convert.ToInt32(cnt.ExecuteScalar());


						trans.Commit();
					}
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			return rowCount;
		}		

		/// <summary>
		/// Deletes the application authentication token.
		/// The application authentication token is deleted upon Logout, Session End, or Application End
		/// </summary>
		private void DeleteAppAuthToken(int id)
		{       
			using(var con = new SqliteConnection(connectionString))
			{
				try
				{                   
					using(var trans = con.BeginTransaction())
					{
						var del = con.CreateCommand();
						del.Transaction = trans;
						del.CommandText = @"DELETE FROM AppAuth
											WHERE Id = $Id";
						del.Parameters.AddWithValue("$Id", id);						

						con.Open();												
						del.ExecuteNonQuery();
						trans.Commit();
					}
				}
				catch(Exception ex)
				{
					throw ex;
				}
			}
			return;
		}

		
	}
		   
		
}
