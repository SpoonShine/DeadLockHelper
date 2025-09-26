using System.Text;

namespace DeadLockHelper
{
    class Program
    {
        class Person
        {
            public Person()
            {
                double btk = BulletsToKill();
                double ttk = TimeToKill();
                double mtk = MagazinesToKill();
                double dps = DamagePerSecond();

                Console.WriteLine(
                $"Пуль для убийства: {btk} "
                + $"\nВремя для убийства: {ttk}с."
                + $"\nОбойм для убийств: {mtk}"
                + $"\nУрон в секунду: {dps}");
            }
            public double BulletsToKill()
            {
                double btk = Math.Ceiling(Health / Damage);
                return btk;
            }
            public double MagazinesToKill()
            {
                double mtk = BulletsToKill() / Ammo;
                return mtk;
            }
            public double DamagePerSecond()
            {
                double dps = 0;
                double remainingTime = 1;
                if (BulletsPerSecond > Ammo && ReloadTime < 1 && remainingTime - ReloadTime - (TimeForOneBullet * (Ammo + 1)) >= 0)
                {
                    double BulletsToShoot = 0;
                    while (remainingTime > 0)
                    {
                        if ((remainingTime - TimeForOneBullet * Ammo) >= 0)
                        {
                            remainingTime -= TimeForOneBullet * Ammo;
                            BulletsToShoot += Ammo;
                            if ((remainingTime - ReloadTime) >= 0)
                            {
                                remainingTime -= ReloadTime;
                                BulletsToShoot += Math.Floor(remainingTime / TimeForOneBullet);
                            }
                            dps += BulletsToShoot * Damage;
                        }
                        else if ((remainingTime - TimeForOneBullet) >= 0)
                        {
                            dps += Math.Floor(remainingTime / TimeForOneBullet) * Damage;
                        }
                    }
                    //remainingTime = remainingTime - ReloadTime - (TimeForOneBullet * Ammo);
                    //double a = (1 - ReloadTime) / BulletsPerSecond;
                    //Console.WriteLine(a);
                    //dps = (Damage * BulletsPerSecond * Ammo) / (Ammo / BulletsPerSecond + ReloadTime);
                    //3 * 3.14 * 5 / (5 / 3.14 + 2.59) = 11.26
                }
                else
                {
                    dps = Damage * Math.Min(Math.Floor(BulletsPerSecond), Ammo);
                }
                return dps;
            }
            public double TimeToKill()
            {
                //Нужно учесть перезарядку,
                //восстановление хп, урон,
                double ttk = Health / (Damage * BulletsPerSecond);
                return ttk;
            }
            //stats
            //TimeToShootFullMagazine
            public double ttsfm => Ammo / BulletsPerSecond;
            public double TimeForOneBullet => 1 / BulletsPerSecond;
            public double Health = 650;
            public double HealthRegen = 2;
            public double Damage = 5;
            public double BulletsPerSecond = 4;
            public double Ammo = 5.0;
            public double BulletSpeed = 254;
            public double ReloadTime = 2.59;
            public double Distance = 15;
        }
        static void Main()
        {
            //Console.WriteLine(15 % 1 == 0);
            Person Adam = new Person();
            Console.WriteLine($"Хп: {Adam.Health}");
            //Console.WriteLine("Hello World!");
        }
    }
}