using LinkBox.Entities;

namespace LinkBox.Models
{
	public  class LinkBoxConfig
	{
		public int Count { get; set; }

		private List<ConfigEntity> _configs = new List<ConfigEntity>();
		public void Set(List<ConfigEntity> configs)
		{
			_configs = configs;
			Count = configs.Count;
		}


		public  string Name
		{
			get
			{
				return _configs.FirstOrDefault(x => x.Name == ConfigField.Name)?.Value ?? "";
			}
		}
	}
}
