// 
// NHibernate.Mapping.Attributes
// This product is under the terms of the GNU Lesser General Public License.
//
//
//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.573
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------
//
//
// This source code was auto-generated by Refly, Version=2.21.1.0 (modified).
//
namespace NHibernate.Mapping.Attributes
{
	
	
	/// <summary> </summary>
	[System.AttributeUsage(System.AttributeTargets.Property | System.AttributeTargets.Field, AllowMultiple=true)]
	[System.Serializable()]
	public class CacheAttribute : BaseAttribute
	{
		
		private CacheUsage _usage = CacheUsage.Unspecified;
		
		/// <summary> Default constructor (position=0) </summary>
		public CacheAttribute() : 
				base(0)
		{
		}
		
		/// <summary> Constructor taking the position of the attribute. </summary>
		public CacheAttribute(int position) : 
				base(position)
		{
		}
		
		/// <summary> </summary>
		public virtual CacheUsage Usage
		{
			get
			{
				return this._usage;
			}
			set
			{
				this._usage = value;
			}
		}
	}
}
