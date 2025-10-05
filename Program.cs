using System;
using System.Text;

namespace DeadLockHelper
{
    class Person
    {
        public Person()
        {
            //double dps = DamagePerSecond();
            Console.WriteLine(
            $"Длительность отхила от Динамо = {DdurationOfHealthRegen}" +
            $"\nДлительность отхила от Вязкуса = {VdurationOfHealthRegen}" +
            $"\nКД отхила от Вязкуса = {VcooldownOfCube}" +
            $"\nКД отхила от Динамо = {DcooldownOfAurora}" +
            $"\nБазовое хп бибопа = {bibopBaseHealth}");
            double DB = 0, VB = 0, MB = 0, NNB = 0;
            int[] DBLevels = new int[maxLevel], VBLevels = new int[maxLevel], MBLevels = new int[maxLevel], NNBLevels = new int[maxLevel];
            string totalDBLevels = "", totalVBLevels = "", totalMBLevels = "", totalNNBLevels = "";
            int DBindex = 0, VBindex = 0, MBindex = 0, NNBindex = 0;
            int DBStreak = 0, VBStreak = 0, MBStreak = 0, NNBStreak = 0;
            while (currentLevel <= maxLevel)
            {
                if (DbaseHealthRegen > VbaseHealthRegen && DbaseHealthRegen > MbaseHealthRegen)
                {
                    if (DBStreak == 0) { DB++; DBLevels[DBindex] = currentLevel; totalDBLevels += DBLevels[DBindex] + " "; DBindex++; }
                    else if (DBStreak > 0) { DB++; DBLevels[DBindex] = currentLevel; totalDBLevels += DBLevels[DBindex] + " "; DBindex++; }
                }
                else if (VbaseHealthRegen > DbaseHealthRegen && VbaseHealthRegen > MbaseHealthRegen) { VB++; VBLevels[VBindex] = currentLevel; totalVBLevels += VBLevels[VBindex] + " "; VBindex++; }
                else if (MbaseHealthRegen > DbaseHealthRegen && MbaseHealthRegen > VbaseHealthRegen) { MB++; MBLevels[MBindex] = currentLevel; totalMBLevels += MBLevels[MBindex] + " "; MBindex++; }
                else if (MbaseHealthRegen == DbaseHealthRegen && MbaseHealthRegen == VbaseHealthRegen) { NNB++; NNBLevels[NNBindex] = currentLevel; totalNNBLevels += MBLevels[NNBindex] + " "; NNBindex++; }
                Console.WriteLine(
                        $"\nТекущий уровень = {currentLevel}" +
                        $"\nСпиритическая мощь Динамо = {DspiritPower}" +
                        $"\nСпиритечкая мощь Вязкуса = {VspiritPower}" +
                        $"\nОтхил от Динамо = {DbaseHealthRegen}" +
                        $"\nОтхил от Вязкуса = {VbaseHealthRegen}\n");
                currentLevel++;
            }
            string finalString = "";
            if (totalVBLevels != "") { finalString += $"Лвла когда Вязкус хиляет лучше всех: {totalVBLevels}\n"; }
            if(totalDBLevels != "") { finalString += $"Лвла когда Динамо хиляет лучше всех: {totalDBLevels}\n"; }
            if(totalMBLevels != "") { finalString += $"Лвла когда Макгиннис хиляет лучше всех: {totalMBLevels}\n"; }
            if(totalNNBLevels != "") { finalString += $"Лвла когда все одинаково хиляют: {totalNNBLevels}\n"; }
            Console.WriteLine(finalString);

            /*Console.WriteLine(
            $"Пуль для убийства: {BulletsToKill} "
            + $"\nВремя для убийства: {ttk}с."
            + $"\nОбойм для убийств: {MagazinesToKill}"
            + $"\nУрон в секунду: {dps}");*/
        }
        public double DamagePerSecond()
        {
            double dps = 0;
            double remainingTime = 1;
            while (remainingTime > 0)
            {
                if (BulletsPerSecond > Ammo && ReloadTime < 1 && remainingTime - ReloadTime - (TimeForOneBullet * (Ammo + 1)) >= 0)
                {
                    Console.WriteLine(3);
                    double BulletsToShoot = 0;
                    remainingTime -= TimeForOneBullet * Ammo + ReloadTime;
                    BulletsToShoot += Ammo;
                    double BulletsInRemTime = Math.Floor(remainingTime / TimeForOneBullet);
                    if (BulletsInRemTime >= 1 && BulletsInRemTime <= Ammo)
                    {
                        Console.WriteLine(6);
                        BulletsToShoot += BulletsInRemTime;
                    }
                    else if (BulletsInRemTime > Ammo)
                    {
                        Console.WriteLine(-6);
                        BulletsToShoot += Math.Floor(TimeForOneBullet * Ammo / remainingTime);
                    }
                    dps += BulletsToShoot * Damage;
                }
                else
                {
                    Console.WriteLine(-3);
                    dps += Damage * Math.Min(Math.Floor(BulletsPerSecond), Ammo);
                    break;
                }
            }
            return dps;
        }
        public double TestingTimeToKill()
        {
            //восстановление хп и т.д.
            //Учесть время на выстрел патрон, после чего добавить хп которое добавилось бы за этот промежуток времени
            double HeroHealth = Health;
            double nrttk = 0;
            double remainingHp = 0;
            //Ещё не готово
            while (HeroHealth > 0)
            {
                remainingHp = HeroHealth / (Damage * Ammo);
                if (remainingHp > 0)
                {
                    HeroHealth -= Damage * Ammo;
                    nrttk += TimeForOneBullet * Ammo;
                    if (HeroHealth > 0) nrttk += ReloadTime;
                }
                else if (remainingHp < 0)
                {
                    HeroHealth = HeroHealth / Damage;
                }
            }
            double ttk = BulletsToKill * TimeForOneBullet + MagazinesToKill * ReloadTime;
            return ttk;
        }
        //stats
        //TimeToShootFullMagazine = ttsfm
        public double ttk => BulletsToKill * TimeForOneBullet + MagazinesToKill * ReloadTime;
        public double BulletsToKill => Math.Ceiling(Health / Damage);
        public double MagazinesToKill => Math.Ceiling(BulletsToKill / Ammo);
        public double ttsfm => Ammo / BulletsPerSecond;
        public double TimeForOneBullet => 1 / BulletsPerSecond;
        public double Health = 650;
        public double HealthRegen = 2;
        public double Damage = 5;
        public double BulletsPerSecond = 5;
        public double Ammo = 5.0;
        public double BulletSpeed = 254;
        public double ReloadTime = 0.1;
        public double Distance = 15;

        public double MbaseHealthRegen = 25;
        public double McooldownOfSpecter = 48;//48s

        public double VbaseHealthRegen => 30 + 0.14 * VspiritPower;
        // 55 will be with AP = 3
        public double VcooldownOfCube = 42; //21s
        public double VdurationOfHealthRegen = 4;//4s
        public double VspiritPower => 1.1 * currentLevel;

        public double DcooldownOfAurora = 48; //34s
        public double DdurationOfHealthRegen = 5;//4s
        public double DbaseHealthRegen => 25 + 0.4 * DspiritPower;
        public double DspiritPower => 1.1 * currentLevel;

        public double bibopBaseHealth => 800 + 54 * currentLevel;

        public int currentLevel = 1;
        public int maxLevel = 34; // bcs lvl starts at 0 in game, but i dont want to 
        // total AP = 30
        // AP to max one skill = 8
        public double AP = 0; // 1, 3, 5, 7 -> 33
        // not gaining AP on levels 0 | 2 | 4 | 6
        static void Main()
        {
            Person Adam = new Person();
            Console.WriteLine($"Хп: {Adam.Health}");
        }
    }
}