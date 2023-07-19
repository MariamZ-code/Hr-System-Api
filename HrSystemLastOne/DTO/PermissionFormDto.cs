namespace HrSystemLastOne.DTO
{
    public class PermissionFormDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public List<CheckBoxDto> RoleClaims { get; set; }
    }
}
