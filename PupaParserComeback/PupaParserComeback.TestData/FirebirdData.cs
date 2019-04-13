using PupaParserComeback.Data.Firebird.Dto;
using System;

namespace PupaParserComeback.TestData
{
    public static class FirebirdData
    {
        public static PRIZ Build(int id)
        {
            return new PRIZ()
            {
                // service info
                ID = id,
                RVK = "Барабинский",
                D_PRIB = new DateTime(2017, 1, 1),

                // criminal info
                NA_UCHETE = "Не состоял",
                SUD = "Не имеет",

                // medicine info
                GODN = "А",
                P_PREDN = 2,
                TDT = "2,4-10",
                STAT = "13д",
                ZREN = @"1,0\1,0",
                ROST = 180,
                MASSA = 80,
                R_G_U = "59",
                R_O_G = @"48\3",
                R_OB = "43",
                IMEET_RAZR = 0,

                // passport info
                KEM_VIDAN = "ОУФМС РФ по НСО в Ленинском районе",
                D_PASPORT = new DateTime(2016, 1, 1),
                FAM = "Иванов" + id,
                IM = "Иван" + id,
                OTCH = "Иванович" + id,
                D_ROD = new DateTime(1999, 1, 1),
                M_ROD = "НСО, г. Новосибирск",
                S_PASPORT = "5013",
                N_PASPORT = "055467",
                BRAK = "Холост",
                IMEET_REB = 0,

                // military info
                // personal number
                LN_SER = "ВС",
                LN_NUM = "052572",
                // military bilet
                S_V_BIL = "АН",
                N_V_BIL = "2281488",
                F_DOP = 1,
                N_DOP = "1488",
                D_DOP = new DateTime(2017, 1, 1),
                // proficiency card
                PROF_P = "I",
                NPU = "Высокая",
                // team mode
                REZH_KOM = "НЕРЕЖ.",

                // education and work
                OBRAZOV = "Основное",
                DO_PRIZ = "Нигде не работал и не учился",

                // family info
                ODIN_ROD = 0,
                BEZ_ROD = 0,

                // driver info
                S_VA = "54CE",
                N_VA = "625478",
                SPEC = "ОСТ",
            };
        }
    }
}
