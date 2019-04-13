using System;

namespace PupaParserComeback.Data.SQLite.Dto
{
    public partial class priz : IEquatable<priz>
    {
        public override bool Equals(object obj)
        {
            var source = obj as priz;
            if (source == null) return false;

            return Equals(source);
        }
        public override int GetHashCode()
        {
            return id.GetHashCode() ^
                   fb_id.GetHashCode() ^
                   rvk.GetHashCode() ^
                   d_advent.GetHashCode() ^
                   percent.GetHashCode() ^

                   accounting.GetHashCode() ^
                   gangsta.GetHashCode() ^

                   rank.GetHashCode() ^
                   tdt.GetHashCode() ^
                   article.GetHashCode() ^
                   eye.GetHashCode() ^
                   blood_type.GetHashCode() ^
                   height.GetHashCode() ^
                   mass.GetHashCode() ^
                   head.GetHashCode() ^
                   clothes.GetHashCode() ^
                   shoes.GetHashCode() ^
                   sport.GetHashCode() ^
                   kind_of_sport.GetHashCode() ^

                   g_pass.GetHashCode() ^
                   kod_g_pass.GetHashCode() ^
                   d_pass.GetHashCode() ^
                   surname.GetHashCode() ^
                   name.GetHashCode() ^
                   patr_name.GetHashCode() ^
                   born_date.GetHashCode() ^
                   born_place.GetHashCode() ^
                   pass.GetHashCode() ^
                   register_location.GetHashCode() ^
                   actually_location.GetHashCode() ^
                   locality.GetHashCode() ^
                   family_status.GetHashCode() ^
                   baby.GetHashCode() ^

                   l_n.GetHashCode() ^
                   v_b.GetHashCode() ^
                   f_access.GetHashCode() ^
                   n_access.GetHashCode() ^
                   d_access.GetHashCode() ^
                   ppo.GetHashCode() ^
                   pp_appointment.GetHashCode() ^
                   npu.GetHashCode() ^
                   ops.GetHashCode() ^
                   destination.GetHashCode() ^

                   education.GetHashCode() ^
                   spec.GetHashCode() ^
                   activity.GetHashCode() ^

                   modile_phone.GetHashCode() ^
                   home_phone.GetHashCode() ^

                   parents.GetHashCode() ^

                   relation.GetHashCode() ^
                   relative_name.GetHashCode() ^
                   relative_birth_date.GetHashCode() ^
                   relative_birth_place.GetHashCode() ^
                   relative_work_place.GetHashCode() ^

                   relation2.GetHashCode() ^
                   relative_name2.GetHashCode() ^
                   relative_birth_date2.GetHashCode() ^
                   relative_birth_place2.GetHashCode() ^
                   relative_work_place2.GetHashCode() ^

                   relation3.GetHashCode() ^
                   relative_name3.GetHashCode() ^
                   relative_birth_date3.GetHashCode() ^
                   relative_birth_place3.GetHashCode() ^
                   relative_work_place3.GetHashCode() ^

                   va.GetHashCode() ^
                   va_date.GetHashCode() ^
                   vus_va.GetHashCode();
        }

        public bool Equals(priz other)
        {
            return id == other.id &&
                   fb_id == other.fb_id &&
                   rvk == other.rvk &&
                   d_advent == other.d_advent &&
                   percent == other.percent &&
                
                   accounting == other.accounting &&
                   gangsta == other.gangsta &&
                
                   rank == other.rank &&
                   tdt == other.tdt &&
                   article == other.article &&
                   eye == other.eye &&
                   blood_type == other.blood_type &&
                   height == other.height &&
                   mass == other.mass &&
                   head == other.head &&
                   clothes == other.clothes &&
                   shoes == other.shoes &&
                   sport == other.sport &&
                   kind_of_sport == other.kind_of_sport &&
                
                   g_pass == other.g_pass &&
                   kod_g_pass == other.kod_g_pass &&
                   d_pass == other.d_pass &&
                   surname == other.surname &&
                   name == other.name &&
                   patr_name == other.patr_name &&
                   born_date == other.born_date &&
                   born_place == other.born_place &&
                   pass == other.pass &&
                   register_location == other.register_location &&
                   actually_location == other.actually_location &&
                   locality == other.locality &&
                   family_status == other.family_status &&
                   baby == other.baby &&
                
                   l_n == other.l_n &&
                   v_b == other.v_b &&
                   f_access == other.f_access &&
                   n_access == other.n_access &&
                   d_access == other.d_access &&
                   ppo == other.ppo &&
                   pp_appointment == other.pp_appointment &&
                   npu == other.npu &&
                   ops == other.ops &&
                   destination == other.destination &&
                
                   education == other.education &&
                   spec == other.spec &&
                   activity == other.activity &&
                
                   modile_phone == other.modile_phone &&
                   home_phone == other.home_phone &&
                
                   parents == other.parents &&

                   relation == other.relation &&
                   relative_name == other.relative_name &&
                   relative_birth_date == other.relative_birth_date &&
                   relative_birth_place == other.relative_birth_place &&
                   relative_work_place == other.relative_work_place &&

                   relation2 == other.relation2 &&
                   relative_name2 == other.relative_name2 &&
                   relative_birth_date2 == other.relative_birth_date2 &&
                   relative_birth_place2 == other.relative_birth_place2 &&
                   relative_work_place2 == other.relative_work_place2 &&

                   relation3 == other.relation3 &&
                   relative_name3 == other.relative_name3 &&
                   relative_birth_date3 == other.relative_birth_date3 &&
                   relative_birth_place3 == other.relative_birth_place3 &&
                   relative_work_place3 == other.relative_work_place3 &&
                
                   va == other.va &&
                   va_date == other.va_date &&
                   vus_va == other.vus_va;
        }
    }
}
