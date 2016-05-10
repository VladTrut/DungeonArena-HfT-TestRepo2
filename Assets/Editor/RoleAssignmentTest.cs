using UnityEngine;
using System.Collections;
using NUnit.Framework;

namespace UnityTest 
{
	/// <summary>
	/// Tests of the role assignment.
	/// </summary>
	/// <author>Marcel Mayer</author>
	public class RoleAssignmentTest 
	{
		private NetworkMan networkman;

		[SetUp]
		public void SetUp()
		{
			networkman = new NetworkMan ();
		}

		[Test]
		public void TestCreateNewRole()
		{
			string role1 = "Role1";
			networkman.CreateNewRole (role1);
			Assert.Contains (new Role(role1), networkman.roleList);

			string role2 = "Role2";
			networkman.CreateNewRole (role2);
			Assert.Contains (new Role(role1), networkman.roleList);
			Assert.Contains (new Role(role2), networkman.roleList);

			string role3 = "Role3";
			networkman.CreateNewRole (role3);
			Assert.Contains (new Role(role1), networkman.roleList);
			Assert.Contains (new Role(role2), networkman.roleList);
			Assert.Contains (new Role(role3), networkman.roleList);

			string role4 = "Role4";
			networkman.CreateNewRole (role4);
			Assert.Contains (new Role(role1), networkman.roleList);
			Assert.Contains (new Role(role2), networkman.roleList);
			Assert.Contains (new Role(role4), networkman.roleList);
			Assert.Contains (new Role(role3), networkman.roleList);
		}

		[Test]
		public void TestInitRoleList()
		{
			networkman.InitRoleList();
			Assert.Contains (new Role(NetworkMan.boss), networkman.roleList);
			Assert.Contains (new Role(NetworkMan.closeCombatFighter), networkman.roleList);
			Assert.Contains (new Role(NetworkMan.distanceFighter), networkman.roleList);
			Assert.Contains (new Role(NetworkMan.magicDude), networkman.roleList);
		}

		[Test]
		public void TestGetRandomRoleID()
		{
			networkman.InitRoleList();

			Role role = networkman.GetRandomRole (5);

			Assert.AreEqual (role.Id, 5);
		}

		[Test]
		public void TestGetRandomRoleName()
		{
			networkman.InitRoleList();

			Role role = networkman.GetRandomRole (5);

			Assert.Contains (role.Name, new string[] {
				NetworkMan.boss,
				NetworkMan.closeCombatFighter,
				NetworkMan.distanceFighter,
				NetworkMan.magicDude
			});
		}

		[Test]
		public void TestGetRandomRoleNextRoleID()
		{
			networkman.InitRoleList();

			networkman.GetRandomRole (5);
			Assert.AreEqual (networkman.nextRole, 1);

			networkman.GetRandomRole (5);
			Assert.AreEqual (networkman.nextRole, 2);

			networkman.GetRandomRole (5);
			Assert.AreEqual (networkman.nextRole, 3);

			networkman.GetRandomRole (5);
			Assert.AreEqual (networkman.nextRole, 4);
		}

		[Test]
		public void TestInsertRoleNextRoleID()
		{
			networkman.InitRoleList();

			networkman.GetRandomRole (6);
			networkman.InsertRole (6);

			Assert.AreEqual (networkman.nextRole, 0);
		}

		[Test]
		public void TestInsertRolePosition()
		{
			networkman.InitRoleList();

			Role role = networkman.GetRandomRole (6);
			networkman.InsertRole (6);

			Assert.AreEqual (role, networkman.roleList [3]);
		}

		[TearDown]
		public void TearDown()
		{
			networkman = null;
		}
	}
}
