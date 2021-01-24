using RecapDemo1.Business.Abstract;
using RecapDemo1.Business.Concrete;
using RecapDemo1.Business.Concrete.Platforms;
using RecapDemo1.Business.Concrete.Sales;
using RecapDemo1.Business.ValidationRules.Abstract;
using RecapDemo1.Business.ValidationRules.Concrete.Mernis;
using RecapDemo1.DataAccess.Abstract;
using RecapDemo1.DataAccess.Concrete.Database;
using RecapDemo1.DataAccess.Concrete.File;
using RecapDemo1.Entities;
using RecapDemo1.Entities.Concrete;
using System;
using System.Collections.Generic;

namespace RecapDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            ILoggerService fileLogger = new FileLoggerService();
            ILoggerService databaseLogger = new DatabaseLoggerService();
            IValidateService mernisValidateService = new MernisValidateService();

            IEntityService playerManager = new PlayerManager(new List<ILoggerService> { databaseLogger,fileLogger},
                new List<IValidateService> { mernisValidateService });
            Person mbagdat = new Player() { NationalityNumber = "00000000000", Name = "Mutlu", 
                LastName = "Bağdat", YearOfBirth = 1985, Balance = 4000 };
            Person ebrum = new Player() { NationalityNumber = "11111111111", Name = "Ebru",
                LastName = "Bağdat", YearOfBirth = 2000 , Balance = 1500};
            Person bona = new Player() { NationalityNumber = "22222222222", Name = "bona",
                LastName = "fides", YearOfBirth = 2003 , Balance= 99999};
            Person cnn = new Player() { NationalityNumber = "33333333333", Name = "Canan",
                LastName = "fide", YearOfBirth = 2000 , Balance = 2200};

            playerManager.Add(mbagdat);
            playerManager.Add(ebrum);
            playerManager.Add(bona);
            playerManager.Add(cnn);


            IEntity godOfWar = new Game() { Id = 1, Name = "GOD OF WAR", Publisher = "Rockstar Games", UnitPrice = 140.50 };
            IEntity wow = new Game() { Id = 2, Name = "World of Warcraft", Publisher = "Blizzard", UnitPrice = 2000};
            IEntity cyberpunk = new Game() { Id = 3, Name = "Cyberpunk 2077", Publisher = "CD Projekt Red ", UnitPrice = 239.99 };

            IEntityService gameManager = new GameManager(new List<ILoggerService> { databaseLogger });
            gameManager.Add(godOfWar);
            gameManager.Add(wow);
            gameManager.Add(cyberpunk);

            ISalesService yaz_indirimi = new SummerSalesManager();
            ISalesService karaCuma_indirimi = new BlackFridaySalesManager();
            ISalesService yilbasi_indirimi = new NewYearSalesManager();
            ISalesService haftasonu_indirimi = new WeekendSalesManager();

            IPlatformService steam = new SteamPlatformManager(new List<ISalesService> { });
            IPlatformService epic_games = 
                new EpicGamesPlatformManager(new List<ISalesService> {karaCuma_indirimi,haftasonu_indirimi });

            steam.BuyGame(ebrum,godOfWar);
            epic_games.BuyGame(mbagdat, cyberpunk);
            steam.ToRefund(ebrum, godOfWar);
            epic_games.BuyGame(ebrum, godOfWar);


        }
    }
}
