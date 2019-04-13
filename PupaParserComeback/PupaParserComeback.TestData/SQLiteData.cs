using PupaParserComeback.Data.SQLite.Dto;
using PupaParserComeback.Domain.Constants;
using System;

namespace PupaParserComeback.TestData
{
    public static class SQLiteData
    {
        public static priz Build(int sqliteId, int formId, string photoExtension)
        {
            return new priz()
            {
                // service info
                id = sqliteId,
                fb_id = formId.ToString(),
                rvk = "Барабинский",
                d_advent = new DateTime(2017, 1, 1).ToString(DateConstants.RecruitDateFormat),
                percent = 100,

                // criminal info
                accounting = "Не состоял",
                gangsta = "Не имеет",

                // medicine info
                rank = "А-2",
                tdt = "2,4-10",
                article = "13д",
                eye = "1,0/1,0",
                blood_type = "1+",
                // physiological characteristics
                height = "180",
                mass = "80",
                head = "59",
                clothes = "48/3",
                shoes = "43",
                // sport info
                sport = "Не имеет",
                kind_of_sport = "Футбол",

                // passport info
                g_pass = "ОУФМС РФ по НСО в Ленинском районе",
                kod_g_pass = "540007",
                d_pass = new DateTime(2016, 1, 1).ToString(DateConstants.RecruitDateFormat),
                surname = $"Иванов{sqliteId}",
                name = $"Иван{sqliteId}",
                patr_name = $"Иванович{sqliteId}",
                born_date = new DateTime(1999, 1, 1).ToString(DateConstants.RecruitDateFormat),
                born_place = "НСО, г. Новосибирск",
                pass = "5013 055467",
                register_location = "Новосибирская обл., г. Новосибирск, ул. Валдайская, д. 19/1",
                actually_location = "Новосибирская обл., г. Новосибирск, ул. Валдайская, д. 19/1",
                locality = "г. Новосибирск",
                family_status = "Холост",
                baby = "Не имеет",
                photo = $"{sqliteId}{photoExtension}",

                // military info
                // personal number
                l_n = "ВС 052572",
                // military bilet
                v_b = "АН 2281488",
                f_access = "1",
                n_access = "1488",
                d_access = new DateTime(2017, 1, 1).ToString(DateConstants.RecruitDateFormat),
                // proficiency card
                ppo = "I",
                pp_appointment = "Командные",
                npu = "Высокая",
                ops = "Высокий",
                //team mode
                destination = "НЕРЕЖ.",

                // education and work
                education = "Основное",
                spec = "Повар",
                activity = "Нигде не работал и не учился",

                // contacts
                modile_phone = "9517925897",
                home_phone = "9517925897",

                // family info
                parents = "Полная",

                relation = "Отец",
                relative_name = "Грибко Иван Викторович",
                relative_birth_date = new DateTime(1960, 1, 1).ToString(DateConstants.RecruitDateFormat),
                relative_birth_place = "Новосибирская обл., г. Новосибирск",
                relative_work_place = "Пекарь",

                relation2 = "Мать",
                relative_name2 = "Грибко Юлия Вадимовна",
                relative_birth_date2 = new DateTime(1965, 1, 1).ToString(DateConstants.RecruitDateFormat),
                relative_birth_place2 = "Новосибирская обл., г. Новосибирск",
                relative_work_place2 = "Повар",

                relation3 = "Сестра",
                relative_name3 = "Грибко Валерия Ивановна",
                relative_birth_date3 = new DateTime(1990, 1, 1).ToString(DateConstants.RecruitDateFormat),
                relative_birth_place3 = "Новосибирская обл., г. Новосибирск",
                relative_work_place3 = "Художник",

                // driver info
                va = "54CE 625478",
                va_date = new DateTime(2016, 1, 1).ToString(DateConstants.RecruitDateFormat),
                vus_va = "ОСТ",
            };
        }
    }
}
