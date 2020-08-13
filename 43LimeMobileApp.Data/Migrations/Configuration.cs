namespace _43LimeMobileApp.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using _43LimeMobileApp.Models.Entities;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            Role admin = new Role("Administrator");
            Role reportUser = new Role("Report User");
            Role user = new Role("Operator");
            
            context.Roles.AddOrUpdate(admin);
            context.Roles.AddOrUpdate(reportUser);
            context.Roles.AddOrUpdate(user);

            ButtonCommand command1 = new ButtonCommand(1, "Log In");
            ButtonCommand command2 = new ButtonCommand(2, "Log Out");
            ButtonCommand command3 = new ButtonCommand(3, "Start Day");
            ButtonCommand command4 = new ButtonCommand(4, "End Day");
            ButtonCommand command5 = new ButtonCommand(5, "Start Lunch");
            ButtonCommand command6 = new ButtonCommand(6, "End Lunch");
            ButtonCommand command10 = new ButtonCommand(10, "SELECT LOADER");
            ButtonCommand command11 = new ButtonCommand(11, "Load Scalper", 10);
            ButtonCommand command12 = new ButtonCommand(12, "Load Truck", 10);
            ButtonCommand command13 = new ButtonCommand(13, "Move Material", 10);
            ButtonCommand command14 = new ButtonCommand(14, "Fork Work", 10);
            ButtonCommand command15 = new ButtonCommand(15, "Equipment Issue", 10);
            ButtonCommand command20 = new ButtonCommand(20, "SELECT WATER TRUCK");
            ButtonCommand command21 = new ButtonCommand(21, "Fill Truck", 20);
            ButtonCommand command22 = new ButtonCommand(22, "Water Road", 20);
            ButtonCommand command23 = new ButtonCommand(23, "Equipment Issue", 20);
            ButtonCommand command30 = new ButtonCommand(30, "SELECT TRACTOR");
            ButtonCommand command31 = new ButtonCommand(31, "Road Work", 30);
            ButtonCommand command32 = new ButtonCommand(32, "Clean Up", 30);
            ButtonCommand command33 = new ButtonCommand(33, "Fork Work", 30);
            ButtonCommand command34 = new ButtonCommand(34, "Equipment Issue", 30);

            context.Commands.AddOrUpdate(command1);
            context.Commands.AddOrUpdate(command2);
            context.Commands.AddOrUpdate(command3);
            context.Commands.AddOrUpdate(command4);
            context.Commands.AddOrUpdate(command5);
            context.Commands.AddOrUpdate(command6);
            context.Commands.AddOrUpdate(command10);
            context.Commands.AddOrUpdate(command11);
            context.Commands.AddOrUpdate(command12);
            context.Commands.AddOrUpdate(command13);
            context.Commands.AddOrUpdate(command14);
            context.Commands.AddOrUpdate(command15);
            context.Commands.AddOrUpdate(command20);
            context.Commands.AddOrUpdate(command21);
            context.Commands.AddOrUpdate(command22);
            context.Commands.AddOrUpdate(command23);
            context.Commands.AddOrUpdate(command30);
            context.Commands.AddOrUpdate(command31);
            context.Commands.AddOrUpdate(command32);
            context.Commands.AddOrUpdate(command33);
            context.Commands.AddOrUpdate(command34);

            User adminUser = new User("0001", "Administrator", 1, true, false);
            User report = new User("0002", "Report User", 2, true, false);
            User userUser = new User("0003", "Operator", 3, true, false);

            context.Users.AddOrUpdate(adminUser);
            context.Users.AddOrUpdate(report);
            context.Users.AddOrUpdate(userUser);


        }
    }
}
