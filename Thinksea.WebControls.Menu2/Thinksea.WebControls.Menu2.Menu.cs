namespace Thinksea.WebControls.Menu2
{
	/// <summary>
	/// 两级菜单类。
	/// </summary>
	public class Menu
	{
		/// <summary>
		/// XML 格式的菜单数据库文件。
		/// </summary>
		private string FileName = "";
		/// <summary>
		/// 菜单的 XML 格式数据源。
		/// </summary>
		private System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();

		/// <summary>
		/// 获取或设置菜单组默认展开状态。
		/// </summary>
		public bool DefaultExpand
		{
			get
			{
				bool DefaultExpand = true;
				System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu" );
				foreach( System.Xml.XmlNode tmp in xnl )
				{
					if( tmp.Attributes["DefaultExpand"] != null )
					{
						DefaultExpand = System.Convert.ToBoolean(tmp.Attributes["DefaultExpand"].Value);
					}

				}

				return DefaultExpand;
			}
			set
			{
				System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu" );
				foreach( System.Xml.XmlNode tmp in xnl )
				{
					System.Xml.XmlAttribute xa = this.xmlDocument.CreateAttribute("DefaultExpand");
					xa.Value = value.ToString();
					tmp.Attributes.SetNamedItem(xa);

				}

			}
		}

		/// <summary>
		/// 获取或设置菜单项 URL 默认打开方式。
		/// </summary>
		public string DefaultTarget
		{
			get
			{
				string DefaultTarget = "_self";
				System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu" );
				foreach( System.Xml.XmlNode tmp in xnl )
				{
					if( tmp.Attributes["DefaultTarget"] != null )
					{
						DefaultTarget = tmp.Attributes["DefaultTarget"].Value;
					}

				}

				return DefaultTarget;
			}
			set
			{
				System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu" );
				foreach( System.Xml.XmlNode tmp in xnl )
				{
					System.Xml.XmlAttribute xa = this.xmlDocument.CreateAttribute("DefaultTarget");
					xa.Value = value;
					tmp.Attributes.SetNamedItem(xa);

				}

			}
		}


		/// <summary>
		/// 初始化此实例。
		/// </summary>
		/// <param name="filename">XML 格式的菜单数据库文件。</param>
		public Menu( string filename )
		{
			this.FileName = filename;
			this.xmlDocument.Load( filename );

		}


		/// <summary>
		/// 验证指定的 ID 对应的记录是否菜单项还是菜单组。
		/// </summary>
		/// <param name="ID">菜单项或菜单组 ID。</param>
		/// <returns>true 表示菜单项；false 表示菜单组。</returns>
		/// <exception cref="Thinksea.NotFoundException">没有找到指定的记录。</exception>
		public bool IsMenuItem( string ID )
		{
			if( this.GetMenuItem( ID ) != null ) return true;
			if( this.GetMenuGroup( ID ) != null ) return false;

			throw new Thinksea.NotFoundException("没有找到指定的记录。");

		}

		/// <summary>
		/// 获取菜单组。
		/// </summary>
		/// <param name="ID">菜单组 ID。</param>
		/// <returns>一个 MenuGroupInfo 实例。否则返回 null。</returns>
		public MenuGroup GetMenuGroup( string ID )
		{
			bool dExpand = this.DefaultExpand;
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					string GID = "";
					if( tmp.Attributes["ID"] != null )
					{
						GID = tmp.Attributes["ID"].Value;
					}
					string Text = "";
					if( tmp.Attributes["Text"] != null )
					{
						Text = tmp.Attributes["Text"].Value;
					}
					bool Expand = dExpand;
					if( tmp.Attributes["Expand"] != null )
					{
						Expand = System.Convert.ToBoolean(tmp.Attributes["Expand"].Value);
					}
					MenuGroup mgi = new MenuGroup( GID, Text, Expand );
					return mgi;
				}

			}

			return null;

		}

		/// <summary>
		/// 获取菜单组集合。
		/// </summary>
		/// <returns>一个 MenuGroupInfo 数组。</returns>
		public MenuGroup [] GetMenuGroup( )
		{
			System.Collections.ArrayList result = new System.Collections.ArrayList();

			bool dExpand = this.DefaultExpand;
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				string GID = "";
				if( tmp.Attributes["ID"] != null )
				{
					GID = tmp.Attributes["ID"].Value;
				}
				string Text = "";
				if( tmp.Attributes["Text"] != null )
				{
					Text = tmp.Attributes["Text"].Value;
				}
				bool Expand = dExpand;
				if( tmp.Attributes["Expand"] != null )
				{
					Expand = System.Convert.ToBoolean(tmp.Attributes["Expand"].Value);
				}
				result.Add( new MenuGroup( GID, Text, Expand ) );

			}
			return (MenuGroup [])(result.ToArray(typeof(MenuGroup)));

		}

		/// <summary>
		/// 获取菜单项。
		/// </summary>
		/// <param name="ID">菜单项 ID。</param>
		/// <returns>一个 MenuItemInfo 实例。否则返回 null。</returns>
		public MenuItem GetMenuItem( string ID )
		{
			string dTarget = this.DefaultTarget;
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup/MenuItem" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					string GID = "";
					if( tmp.Attributes["ID"] != null )
					{
						GID = tmp.Attributes["ID"].Value;
					}
					string Text = "";
					if( tmp.Attributes["Text"] != null )
					{
						Text = tmp.Attributes["Text"].Value;
					}
					string URL = "";
					if( tmp.Attributes["URL"] != null )
					{
						URL = tmp.Attributes["URL"].Value;
					}
					string Target = dTarget;
					if( tmp.Attributes["Target"] != null )
					{
						Target = tmp.Attributes["Target"].Value;
					}
					System.Collections.ArrayList Access = new System.Collections.ArrayList();
					if( tmp.Attributes["Access"] != null )
					{
						string [] acs = tmp.Attributes["Access"].Value.Split(';');
						foreach( string tmpacs in acs )
						{
							if( tmpacs.Length > 0 )
							{
								Access.Add( tmpacs );
							}
						}
					}
					MenuItem mii = new MenuItem( GID, Text, URL, Target, (string [])(Access.ToArray(typeof(string))) );
					return mii;
				}

			}

			return null;

		}

		/// <summary>
		/// 获取菜单项集合。
		/// </summary>
		/// <param name="MenuGroupID">菜单组 ID。</param>
		/// <returns>一个 MenuItemInfo 数组。</returns>
		public MenuItem [] GetMenuItemOfMenuGroupID( string MenuGroupID )
		{
			System.Collections.ArrayList result = new System.Collections.ArrayList();

			string dTarget = this.DefaultTarget;
			System.Xml.XmlNodeList xnlMenuGroup = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmpMenuGroup in xnlMenuGroup )
			{
				if( tmpMenuGroup.Attributes["ID"] != null && tmpMenuGroup.Attributes["ID"].Value == MenuGroupID )
				{
					foreach( System.Xml.XmlNode tmp in tmpMenuGroup.ChildNodes )
					{
						string GID = "";
						if( tmp.Attributes["ID"] != null )
						{
							GID = tmp.Attributes["ID"].Value;
						}
						string Text = "";
						if( tmp.Attributes["Text"] != null )
						{
							Text = tmp.Attributes["Text"].Value;
						}
						string URL = "";
						if( tmp.Attributes["URL"] != null )
						{
							URL = tmp.Attributes["URL"].Value;
						}
						string Target = dTarget;
						if( tmp.Attributes["Target"] != null )
						{
							Target = tmp.Attributes["Target"].Value;
						}
						System.Collections.ArrayList Access = new System.Collections.ArrayList();
						if( tmp.Attributes["Access"] != null )
						{
							string [] acs = tmp.Attributes["Access"].Value.Split(';');
							foreach( string tmpacs in acs )
							{
								if( tmpacs.Length > 0 )
								{
									Access.Add( tmpacs );
								}
							}
						}
						result.Add( new MenuItem( GID, Text, URL, Target, (string [])(Access.ToArray(typeof(string))) ) );

					}
				}

			}

			return (MenuItem [])(result.ToArray(typeof(MenuItem)));

		}

		/// <summary>
		/// 获取指定的权限集允许访问的菜单项集合。
		/// </summary>
		/// <param name="MenuGroupID">菜单组 ID。</param>
		/// <param name="AccessFilter">访问权限集合。</param>
		/// <returns>一个 MenuItemInfo 数组。</returns>
		public MenuItem [] GetMenuItemOfMenuGroupIDWithAccessFilter( string MenuGroupID, string [] AccessFilter )
		{
			System.Collections.ArrayList result = new System.Collections.ArrayList();

			string dTarget = this.DefaultTarget;
			System.Xml.XmlNodeList xnlMenuGroup = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmpMenuGroup in xnlMenuGroup )
			{
				if( tmpMenuGroup.Attributes["ID"] != null && tmpMenuGroup.Attributes["ID"].Value == MenuGroupID )
				{
					foreach( System.Xml.XmlNode tmp in tmpMenuGroup.ChildNodes )
					{
						string GID = "";
						if( tmp.Attributes["ID"] != null )
						{
							GID = tmp.Attributes["ID"].Value;
						}
						string Text = "";
						if( tmp.Attributes["Text"] != null )
						{
							Text = tmp.Attributes["Text"].Value;
						}
						string URL = "";
						if( tmp.Attributes["URL"] != null )
						{
							URL = tmp.Attributes["URL"].Value;
						}
						string Target = dTarget;
						if( tmp.Attributes["Target"] != null )
						{
							Target = tmp.Attributes["Target"].Value;
						}
						System.Collections.ArrayList Access = new System.Collections.ArrayList();
						if( tmp.Attributes["Access"] != null )
						{
							string [] acs = tmp.Attributes["Access"].Value.Split(';');
							foreach( string tmpacs in acs )
							{
								if( tmpacs.Length > 0 )
								{
									Access.Add( tmpacs );
								}
							}
						}

						MenuItem mi = new MenuItem( GID, Text, URL, Target, (string [])(Access.ToArray(typeof(string))) );
						if( Access.Count > 0 )
						{
							bool iExists = false;
							foreach( string tmp2 in AccessFilter )
							{
								if( Access.Contains( tmp2 ) )
								{
									iExists = true;
									break;
								}
							}
							if( ! iExists )
							{
								continue;
							}
						}

						result.Add( mi );


					}
				}

			}

			return (MenuItem [])(result.ToArray(typeof(MenuItem)));

		}

		/// <summary>
		/// 添加一个菜单组。
		/// </summary>
		/// <param name="ID">菜单组编号。</param>
		/// <param name="Text">菜单组文本。</param>
		/// <param name="Expand">菜单组展开状态。</param>
		/// <exception cref="System.ArgumentOutOfRangeException">ID 不允许为空。</exception>
		/// <exception cref="Thinksea.RepeatException">已经存在具有该 ID 的菜单组。</exception>
		public void AddMenuGroup( string ID, string Text, bool Expand )
		{
			if( ID == "" ) throw new System.ArgumentOutOfRangeException("ID 不允许为空。");
			if( this.GetMenuGroup( ID ) != null )
			{
				throw new Thinksea.RepeatException("已经存在具有该 ID 的菜单组。");
			}

			System.Xml.XmlNode xn = this.xmlDocument.CreateNode( System.Xml.XmlNodeType.Element, "MenuGroup", "" );
			System.Xml.XmlAttribute xaID = xn.Attributes.Append( this.xmlDocument.CreateAttribute( "ID" ) );
			xaID.InnerText = ID;
			System.Xml.XmlAttribute xaText = xn.Attributes.Append( this.xmlDocument.CreateAttribute( "Text" ) );
			xaText.InnerText = Text;
			System.Xml.XmlAttribute xaExpand = xn.Attributes.Append( this.xmlDocument.CreateAttribute( "Expand" ) );
			xaExpand.InnerText = Expand.ToString();

			this.xmlDocument.DocumentElement.AppendChild( xn );

		}

		/// <summary>
		/// 添加一个菜单项到指定的菜单组。
		/// </summary>
		/// <param name="MenuGroupID">菜单组 ID。</param>
		/// <param name="ID">菜单项编号。</param>
		/// <param name="Text">菜单项文本。</param>
		/// <param name="URL">菜单项对应的 URL。</param>
		/// <param name="Target">URL 打开方式。</param>
		/// <param name="Access">访问权限列表。（指示允许访问这个菜单项的用户）</param>
		/// <exception cref="System.ArgumentOutOfRangeException">ID 不允许为空。</exception>
		/// <exception cref="Thinksea.NotFoundException">没有找到指定的菜单组。</exception>
		/// <exception cref="Thinksea.RepeatException">已经存在具有该 ID 的菜单项。</exception>
		public void AddMenuItem( string MenuGroupID, string ID, string Text, string URL, string Target, string [] Access )
		{
			if( ID == "" ) throw new System.ArgumentOutOfRangeException("ID 不允许为空。");
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == MenuGroupID )
				{
					if( this.GetMenuGroup( ID ) != null || this.GetMenuItem( ID ) != null )
					{
						throw new Thinksea.RepeatException("已经存在具有该 ID 的菜单项。");
					}

					System.Xml.XmlNode xn = this.xmlDocument.CreateNode( System.Xml.XmlNodeType.Element, "MenuItem", "" );
					System.Xml.XmlAttribute xaID = xn.Attributes.Append( this.xmlDocument.CreateAttribute( "ID" ) );
					xaID.InnerText = ID;
					System.Xml.XmlAttribute xaText = xn.Attributes.Append( this.xmlDocument.CreateAttribute( "Text" ) );
					xaText.InnerText = Text;
					System.Xml.XmlAttribute xaURL = xn.Attributes.Append( this.xmlDocument.CreateAttribute( "URL" ) );
					xaURL.InnerText = URL;
					System.Xml.XmlAttribute xaTarget = xn.Attributes.Append( this.xmlDocument.CreateAttribute( "Target" ) );
					xaTarget.InnerText = Target;
					System.Xml.XmlAttribute xaAccess = xn.Attributes.Append( this.xmlDocument.CreateAttribute( "Access" ) );
					xaAccess.InnerText = string.Join( ";", Access );

					tmp.AppendChild( xn );
					return;

				}

			}
			throw new Thinksea.NotFoundException("没有找到指定的菜单组。");

		}

		/// <summary>
		/// 修改菜单组。
		/// </summary>
		/// <param name="ID">菜单组编号。</param>
		/// <param name="Text">菜单组文本。</param>
		/// <param name="Expand">菜单组展开状态。</param>
		/// <exception cref="System.ArgumentOutOfRangeException">ID 不允许为空。</exception>
		/// <exception cref="Thinksea.NotFoundException">没有找到指定的菜单组。</exception>
		public void ModifyMenuGroup( string ID, string Text, bool Expand )
		{
			if( ID == "" ) throw new System.ArgumentOutOfRangeException("ID 不允许为空。");
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					System.Xml.XmlAttribute xaText = this.xmlDocument.CreateAttribute( "Text" );
					xaText.InnerText = Text;
					tmp.Attributes.SetNamedItem( xaText );

					System.Xml.XmlAttribute xaExpand = this.xmlDocument.CreateAttribute( "Expand" );
					xaExpand.InnerText = Expand.ToString();
					tmp.Attributes.SetNamedItem( xaExpand );

					return;

				}

			}

			throw new Thinksea.NotFoundException("没有找到指定的菜单组。");

		}

		/// <summary>
		/// 修改菜单项。
		/// </summary>
		/// <param name="ID">菜单项编号。</param>
		/// <param name="Text">菜单项文本。</param>
		/// <param name="URL">菜单项对应的 URL。</param>
		/// <param name="Target">URL 打开方式。</param>
		/// <param name="Access">访问权限列表。（指示允许访问这个菜单项的用户）</param>
		/// <exception cref="System.ArgumentOutOfRangeException">ID 不允许为空。</exception>
		/// <exception cref="Thinksea.NotFoundException">没有找到指定的菜单项。</exception>
		public void ModifyMenuItem( string ID, string Text, string URL, string Target, string [] Access )
		{
			if( ID == "" ) throw new System.ArgumentOutOfRangeException("ID 不允许为空。");
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup/MenuItem" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					System.Xml.XmlAttribute xaText = this.xmlDocument.CreateAttribute( "Text" );
					xaText.InnerText = Text;
					tmp.Attributes.SetNamedItem( xaText );

					System.Xml.XmlAttribute xaURL = this.xmlDocument.CreateAttribute( "URL" );
					xaURL.InnerText = URL;
					tmp.Attributes.SetNamedItem( xaURL );

					System.Xml.XmlAttribute xaTarget = this.xmlDocument.CreateAttribute( "Target" );
					xaTarget.InnerText = Target;
					tmp.Attributes.SetNamedItem( xaTarget );

					System.Xml.XmlAttribute xaAccess = this.xmlDocument.CreateAttribute( "Access" );
					xaAccess.InnerText = string.Join( ";", Access );
					tmp.Attributes.SetNamedItem( xaAccess );

					return;

				}

			}

			throw new Thinksea.NotFoundException("没有找到指定的菜单项。");

		}

		/// <summary>
		/// 获取指定的菜单项所属的菜单组。
		/// </summary>
		/// <param name="MenuItemID">菜单项 ID。</param>
		/// <returns>菜单组 ID。</returns>
		/// <exception cref="Thinksea.NotFoundException">没有找到指定的记录。</exception>
		public string GetMenuItemOwnerID( string MenuItemID )
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup/MenuItem" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == MenuItemID )
				{
					System.Xml.XmlNode Owner = tmp.ParentNode;
					if( Owner != null )
					{
						if( Owner.Attributes["ID"] != null )
						{
							return Owner.Attributes["ID"].Value;
						}
					}
				}

			}
			throw new Thinksea.NotFoundException("没有找到指定的记录。");

		}

		/// <summary>
		/// 设置菜单项所属的菜单组。
		/// </summary>
		/// <param name="MenuItemID">菜单项 ID。</param>
		/// <param name="MenuGroupID">新的菜单组 ID。</param>
		/// <exception cref="Thinksea.NotFoundException">没有找到指定的记录。</exception>
		public void MoveMenuItem( string MenuItemID, string MenuGroupID )
		{
			System.Xml.XmlNode MenuItemNode = null;
			System.Xml.XmlNode OldMenuGroupNode = null;
			#region 获取当前所在的菜单组。
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup/MenuItem" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == MenuItemID )
				{
					MenuItemNode = tmp;
					OldMenuGroupNode = tmp.ParentNode;
				}

			}
			if( OldMenuGroupNode == null || MenuItemNode == null )
			{
				throw new Thinksea.NotFoundException("没有找到指定的记录。");
			}
		}
			#endregion

			System.Xml.XmlNode NewMenuGroupNode = null;
			#region 获取新的菜单组。
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == MenuGroupID )
				{
					NewMenuGroupNode = tmp;
					break;
				}

			}

			if( NewMenuGroupNode == null )
			{
				throw new Thinksea.NotFoundException("没有找到指定的菜单组。");
			}
		}
			#endregion

			#region 排出所设置的菜单组 ID 与当前所属的菜单组 ID 相同的情况。
		{
			if( OldMenuGroupNode.Attributes["ID"] != null && OldMenuGroupNode.Attributes["ID"].Value == MenuGroupID )
			{
				return;
			}
		}
			#endregion

			OldMenuGroupNode.RemoveChild( MenuItemNode );
			NewMenuGroupNode.AppendChild( MenuItemNode );

		}

		/// <summary>
		/// 删除菜单组。
		/// </summary>
		/// <param name="ID">菜单组 ID。</param>
		public void RemoveMenuGroup( string ID )
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					this.xmlDocument.DocumentElement.RemoveChild( tmp );

				}

			}

		}

		/// <summary>
		/// 删除菜单项。
		/// </summary>
		/// <param name="ID">菜单项 ID。</param>
		public void RemoveMenuItem( string ID )
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup/MenuItem" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					tmp.ParentNode.RemoveChild( tmp );

				}

			}

		}

		/// <summary>
		/// 删除记录。
		/// </summary>
		/// <param name="ID">记录 ID。</param>
		public void Remove( string ID )
		{
			try
			{
				if( this.IsMenuItem( ID ) )
				{
					this.RemoveMenuItem( ID );
				}
				else
				{
					this.RemoveMenuGroup( ID );
				}
			}
			catch( Thinksea.NotFoundException )
			{
			}

		}

		/// <summary>
		/// 清除所有的菜单组。
		/// </summary>
		public void Clear()
		{
			this.xmlDocument.DocumentElement.InnerXml = "";

		}

		/// <summary>
		/// 保存对 XML 文档的修改。
		/// </summary>
		public void Save()
		{
			this.xmlDocument.Save( this.FileName );
		}

		/// <summary>
		/// 保存对 XML 文档的修改。
		/// </summary>
		/// <param name="filename">XML 格式的菜单数据库文件。</param>
		public void Save( string filename )
		{
			this.xmlDocument.Save( filename );
		}


		/// <summary>
		/// 将指定的菜单组提升一个位置。
		/// </summary>
		/// <param name="ID">菜单组 ID。</param>
		public void SendMenuGroupToPre( string ID )
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					System.Xml.XmlNode ParentNode = tmp.ParentNode;
					System.Xml.XmlNode PreviousSibling = tmp.PreviousSibling;
					if( ParentNode != null && PreviousSibling != null )
					{
						ParentNode.RemoveChild( tmp );
						ParentNode.InsertBefore( tmp, PreviousSibling );
						return;
					}

				}

			}

		}

		/// <summary>
		/// 将指定的菜单组下降一个位置。
		/// </summary>
		/// <param name="ID">菜单组 ID。</param>
		public void SendMenuGroupToBack( string ID )
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					System.Xml.XmlNode ParentNode = tmp.ParentNode;
					System.Xml.XmlNode NextSibling = tmp.NextSibling;
					if( NextSibling != null && NextSibling != null )
					{
						ParentNode.RemoveChild( tmp );
						ParentNode.InsertAfter( tmp, NextSibling );
						return;
					}

				}

			}

		}

		/// <summary>
		/// 将指定的菜单项提升一个位置。
		/// </summary>
		/// <param name="ID">菜单项 ID。</param>
		public void SendMenuItemToPre( string ID )
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup/MenuItem" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					System.Xml.XmlNode ParentNode = tmp.ParentNode;
					System.Xml.XmlNode PreviousSibling = tmp.PreviousSibling;
					if( ParentNode != null && PreviousSibling != null )
					{
						ParentNode.RemoveChild( tmp );
						ParentNode.InsertBefore( tmp, PreviousSibling );
						return;
					}

				}

			}

		}

		/// <summary>
		/// 将指定的菜单项下降一个位置。
		/// </summary>
		/// <param name="ID">菜单项 ID。</param>
		public void SendMenuItemToBack( string ID )
		{
			System.Xml.XmlNodeList xnl = this.xmlDocument.SelectNodes( "/Menu/MenuGroup/MenuItem" );
			foreach( System.Xml.XmlNode tmp in xnl )
			{
				if( tmp.Attributes["ID"] != null && tmp.Attributes["ID"].Value == ID )
				{
					System.Xml.XmlNode ParentNode = tmp.ParentNode;
					System.Xml.XmlNode NextSibling = tmp.NextSibling;
					if( NextSibling != null && NextSibling != null )
					{
						ParentNode.RemoveChild( tmp );
						ParentNode.InsertAfter( tmp, NextSibling );
						return;
					}

				}

			}

		}

		/// <summary>
		/// 将指定的记录提升一个位置。
		/// </summary>
		/// <param name="ID">记录 ID。</param>
		/// <exception cref="Thinksea.NotFoundException">没有找到指定的记录。</exception>
		public void SendToPre( string ID )
		{
			if( this.IsMenuItem( ID ) )
			{
				this.SendMenuItemToPre( ID );
			}
			else
			{
				this.SendMenuGroupToPre( ID );
			}

		}

		/// <summary>
		/// 将指定的记录下降一个位置。
		/// </summary>
		/// <param name="ID">记录 ID。</param>
		/// <exception cref="Thinksea.NotFoundException">没有找到指定的记录。</exception>
		public void SendToBack( string ID )
		{
			if( this.IsMenuItem( ID ) )
			{
				this.SendMenuItemToBack( ID );
			}
			else
			{
				this.SendMenuGroupToBack( ID );
			}

		}


	}


	/// <summary>
	/// 菜单组。
	/// </summary>
	public class MenuGroup
	{
		/// <summary>
		/// 菜单组编号。
		/// </summary>
		private string _ID;
		/// <summary>
		/// 获取或设置菜单组编号。
		/// </summary>
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if( value == "" ) throw new System.ArgumentOutOfRangeException("Value 不允许为空。");

				this._ID = value;
			}
		}

		/// <summary>
		/// 菜单组文本。
		/// </summary>
		private string _Text;
		/// <summary>
		/// 获取或设置菜单组文本。
		/// </summary>
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				this._Text = value;
			}
		}

		/// <summary>
		/// 菜单组展开状态。
		/// </summary>
		private bool _Expand;
		/// <summary>
		/// 获取或设置菜单组展开状态。
		/// </summary>
		public bool Expand
		{
			get
			{
				return this._Expand;
			}
			set
			{
				this._Expand = value;
			}
		}


		/// <summary>
		/// 初始化新的实例。
		/// </summary>
		/// <param name="ID">菜单组编号。</param>
		/// <param name="Text">菜单组文本。</param>
		/// <param name="Expand">菜单组展开状态。</param>
		public MenuGroup( string ID, string Text, bool Expand )
		{
			if( ID == "" ) throw new System.ArgumentOutOfRangeException("ID 不允许为空。");
			this._ID = ID;
			this._Text = Text;
			this._Expand = Expand;

		}

		/// <summary>
		/// 返回该实例的哈希代码。
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this._ID.GetHashCode();
		}


	}


	/// <summary>
	/// 菜单项。
	/// </summary>
	public class MenuItem
	{
		/// <summary>
		/// 菜单编号。
		/// </summary>
		private string _ID;
		/// <summary>
		/// 获取或设置菜单编号。
		/// </summary>
		public string ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if( value == "" ) throw new System.ArgumentOutOfRangeException("Value 不允许为空。");

				this._ID = value;
			}
		}

		/// <summary>
		/// 菜单项文本。
		/// </summary>
		private string _Text;
		/// <summary>
		/// 获取或设置菜单项文本。
		/// </summary>
		public string Text
		{
			get
			{
				return this._Text;
			}
			set
			{
				this._Text = value;
			}
		}

		/// <summary>
		///菜单项对应的 URL。
		/// </summary>
		private string _URL;
		/// <summary>
		/// 获取或设置菜单项对应的 URL。
		/// </summary>
		public string URL
		{
			get
			{
				return this._URL;
			}
			set
			{
				this._URL = value;
			}
		}

		/// <summary>
		/// URL 打开方式。
		/// </summary>
		private string _Target;
		/// <summary>
		/// 获取或设置 URL 打开方式。
		/// </summary>
		public string Target
		{
			get
			{
				return this._Target;
			}
			set
			{
				this._Target = value;
			}
		}

		/// <summary>
		/// 访问权限列表。（指示允许访问这个菜单项的用户）
		/// </summary>
		private System.Collections.ArrayList _Access = new System.Collections.ArrayList();
		/// <summary>
		/// 获取访问权限列表。（指示允许访问这个菜单项的用户）
		/// </summary>
		public System.Collections.ArrayList Access
		{
			get
			{
				return this._Access;
			}
		}


		/// <summary>
		/// 初始化新的实例。
		/// </summary>
		/// <param name="ID">菜单项编号。</param>
		/// <param name="Text">菜单项文本。</param>
		/// <param name="URL">菜单项对应的 URL。</param>
		/// <param name="Target">URL 打开方式。</param>
		/// <param name="Access">访问权限列表。（指示允许访问这个菜单项的用户）</param>
		public MenuItem( string ID, string Text, string URL, string Target, string [] Access )
		{
			if( ID == "" ) throw new System.ArgumentOutOfRangeException("ID 不允许为空。");
			this._ID = ID;
			this._Text = Text;
			this._URL = URL;
			this._Target = Target;
			this._Access.AddRange( Access );

		}

		/// <summary>
		/// 返回该实例的哈希代码。
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
		{
			return this._ID.GetHashCode();
		}


	}


}
