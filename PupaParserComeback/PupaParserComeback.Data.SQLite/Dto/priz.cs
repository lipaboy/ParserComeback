namespace PupaParserComeback.Data.SQLite.Dto
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("priz")]
    public partial class priz
    {
        public long id { get; set; }

        [StringLength(2147483647)]
        public string surname { get; set; }

        [StringLength(2147483647)]
        public string name { get; set; }

        [StringLength(2147483647)]
        public string patr_name { get; set; }

        public string born_date { get; set; }

        [StringLength(2147483647)]
        public string rvk { get; set; }

        [StringLength(2147483647)]
        public string spec { get; set; }

        [StringLength(2147483647)]
        public string vus_va { get; set; }

        [StringLength(2147483647)]
        public string family_status { get; set; }

        [StringLength(2147483647)]
        public string gangsta { get; set; }

        [StringLength(2147483647)]
        public string education { get; set; }

        [StringLength(2147483647)]
        public string ppo { get; set; }

        [StringLength(2147483647)]
        public string npu { get; set; }

        [StringLength(2147483647)]
        public string destination { get; set; }

        [StringLength(2147483647)]
        public string rank { get; set; }

        [StringLength(2147483647)]
        public string tdt { get; set; }

        [StringLength(2147483647)]
        public string f_access { get; set; }

        [StringLength(2147483647)]
        public string n_access { get; set; }

        public string d_access { get; set; }

        [StringLength(2147483647)]
        public string height { get; set; }

        [StringLength(2147483647)]
        public string mass { get; set; }

        [StringLength(2147483647)]
        public string article { get; set; }

        [StringLength(2147483647)]
        public string v_b { get; set; }

        [StringLength(2147483647)]
        public string eye { get; set; }

        [StringLength(2147483647)]
        public string l_n { get; set; }

        [StringLength(2147483647)]
        public string pass { get; set; }

        public string d_pass { get; set; }

        [StringLength(2147483647)]
        public string sport { get; set; }

        [StringLength(2147483647)]
        public string baby { get; set; }

        [StringLength(2147483647)]
        public string activity { get; set; }

        [StringLength(2147483647)]
        public string accounting { get; set; }

        [StringLength(2147483647)]
        public string va { get; set; }

        [StringLength(2147483647)]
        public string born_place { get; set; }

        [StringLength(2147483647)]
        public string g_pass { get; set; }

        [StringLength(2147483647)]
        public string head { get; set; }

        [StringLength(2147483647)]
        public string clothes { get; set; }

        [StringLength(2147483647)]
        public string shoes { get; set; }

        public string d_advent { get; set; }

        public long? percent { get; set; }

        [StringLength(2147483647)]
        public string parents { get; set; }

        [StringLength(2147483647)]
        public string register_location { get; set; }

        [StringLength(2147483647)]
        public string actually_location { get; set; }

        [StringLength(2147483647)]
        public string home_phone { get; set; }

        [StringLength(2147483647)]
        public string modile_phone { get; set; }

        [StringLength(2147483647)]
        public string fb_id { get; set; }

        [StringLength(2147483647)]
        public string blood_type { get; set; }

        [StringLength(2147483647)]
        public string relation { get; set; }

        [StringLength(2147483647)]
        public string relative_name { get; set; }

        public string relative_birth_date { get; set; }

        [StringLength(2147483647)]
        public string relative_birth_place { get; set; }

        [StringLength(2147483647)]
        public string relative_work_place { get; set; }

        [StringLength(2147483647)]
        public string ops { get; set; }

        [StringLength(2147483647)]
        public string kind_of_sport { get; set; }

        public string va_date { get; set; }

        [StringLength(2147483647)]
        public string pp_appointment { get; set; }

        [StringLength(2147483647)]
        public string photo { get; set; }

        [StringLength(2147483647)]
        public string locality { get; set; }

        [StringLength(2147483647)]
        public string relation2 { get; set; }

        [StringLength(2147483647)]
        public string relative_name2 { get; set; }

        public string relative_birth_date2 { get; set; }

        [StringLength(2147483647)]
        public string relative_birth_place2 { get; set; }

        [StringLength(2147483647)]
        public string relative_work_place2 { get; set; }

        [StringLength(2147483647)]
        public string relation3 { get; set; }

        [StringLength(2147483647)]
        public string relative_name3 { get; set; }

        public string relative_birth_date3 { get; set; }

        [StringLength(2147483647)]
        public string relative_birth_place3 { get; set; }

        [StringLength(2147483647)]
        public string relative_work_place3 { get; set; }

        [StringLength(2147483647)]
        public string kod_g_pass { get; set; }
    }
}
