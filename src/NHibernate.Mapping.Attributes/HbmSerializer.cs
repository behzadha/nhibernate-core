//
// NHibernate.Mapping.Attributes
// This product is under the terms of the GNU Lesser General Public License.
//
namespace NHibernate.Mapping.Attributes
{
	/// <summary>
	/// Build hbm.xml files from entities (Class, Subclass or JoinedSubclass).
	/// </summary>
	public class HbmSerializer
	{
		#region static HbmSerializer Default
		static HbmSerializer _default = new HbmSerializer();

		/// <summary> Gets a static instance of HbmSerializer (if you don't want/need to create an instance). </summary>
		public static HbmSerializer Default
		{
			get { return _default; }
		}
		#endregion


		#region HbmWriter, Validate, WriteDateComment and Error properties
		private HbmWriter _hbmWriter = new HbmWriterEx();
		private bool _validate = false;
		private bool _stop; // used to stop the validation
		private bool _writeDateComment = true;
		private System.Text.StringBuilder _error = new System.Text.StringBuilder();

		/// <summary> Gets or sets the HbmWriter to use. </summary>
		public virtual HbmWriter HbmWriter
		{
			get { return _hbmWriter; }
			set { _hbmWriter = value; }
		}

		/// <summary> Gets or sets whether generated xml files must be validated against NHibernate mapping schema. </summary>
		public virtual bool Validate
		{
			get { return _validate; }
			set { _validate = value; }
		}

		/// <summary> Gets or sets whether generated xml files will contain a comment with the date/time of the generation. </summary>
		public virtual bool WriteDateComment
		{
			get { return _writeDateComment; }
			set { _writeDateComment = value; }
		}

		/// <summary> Gets errors that occur while serializing. </summary>
		public virtual System.Text.StringBuilder Error
		{
			get { return _error; }
		}
		#endregion


		#region HibernateMapping-related properties (attributes and their IsSpecifieds)
		private bool _defaultlazy = true;
		private bool _autoimport = true;
		private string _schema = null;
		private string _defaultaccess = null;
		private string _assembly = null;
		private string _namespace = null;
		private CascadeStyle _defaultcascade = CascadeStyle.Unspecified;

		private bool _defaultlazyIsSpecified = false;
		private bool _autoimportIsSpecified = false;
		private bool _schemaIsSpecified = false;
		private bool _defaultaccessIsSpecified = false;
		private bool _assemblyIsSpecified = false;
		private bool _namespaceIsSpecified = false;
		private bool _defaultcascadeIsSpecified = false;


		/// <summary> Gets or sets this "hibernate-mapping" attribute </summary>
		public bool HbmDefaultLazy
		{
			get
			{
				return _defaultlazy;
			}
			set
			{
				_defaultlazy = value;
				HbmDefaultLazyIsSpecified = true;
			}
		}

		/// <summary> Gets or sets this "hibernate-mapping" attribute </summary>
		public virtual string HbmSchema
		{
			get
			{
				return _schema;
			}
			set
			{
				_schema = value;
				HbmSchemaIsSpecified = true;
			}
		}

		/// <summary> Gets or sets this "hibernate-mapping" attribute </summary>
		public virtual CascadeStyle HbmDefaultCascade
		{
			get
			{
				return _defaultcascade;
			}
			set
			{
				_defaultcascade = value;
				HbmDefaultCascadeIsSpecified = true;
			}
		}

		/// <summary> Gets or sets this "hibernate-mapping" attribute </summary>
		public virtual string HbmDefaultAccess
		{
			get
			{
				return _defaultaccess;
			}
			set
			{
				_defaultaccess = value;
				HbmDefaultAccessIsSpecified = true;
			}
		}

		/// <summary> Gets or sets this "hibernate-mapping" attribute </summary>
		public virtual System.Type HbmDefaultAccessType
		{
			get
			{
				return System.Type.GetType( HbmDefaultAccess );
			}
			set
			{
				if(value.Assembly == typeof(int).Assembly)
					HbmDefaultAccess = value.FullName.Substring(7);
				else
					HbmDefaultAccess = value.FullName + ", " + value.Assembly.GetName().Name;
			}
		}

		/// <summary> Gets or sets this "hibernate-mapping" attribute </summary>
		public virtual bool HbmAutoImport
		{
			get
			{
				return _autoimport;
			}
			set
			{
				_autoimport = value;
				HbmAutoImportIsSpecified = true;
			}
		}

		/// <summary> Gets or sets this "hibernate-mapping" attribute </summary>
		public virtual string HbmNamespace
		{
			get
			{
				return _namespace;
			}
			set
			{
				_namespace = value;
				HbmNamespaceIsSpecified = true;
			}
		}

		/// <summary> Gets or sets this "hibernate-mapping" attribute </summary>
		public virtual string HbmAssembly
		{
			get
			{
				return _assembly;
			}
			set
			{
				_assembly = value;
				HbmAssemblyIsSpecified = true;
			}
		}


		/// <summary> Gets or sets if this "hibernate-mapping" attribute is specified </summary>
		public virtual bool HbmDefaultLazyIsSpecified
		{
			get { return _defaultlazyIsSpecified; }
			set { _defaultlazyIsSpecified = value; }
		}

		/// <summary> Gets or sets if this "hibernate-mapping" attribute is specified </summary>
		public virtual bool HbmAutoImportIsSpecified
		{
			get { return _autoimportIsSpecified; }
			set { _autoimportIsSpecified = value; }
		}

		/// <summary> Gets or sets if this "hibernate-mapping" attribute is specified </summary>
		public virtual bool HbmSchemaIsSpecified
		{
			get { return _schemaIsSpecified; }
			set { _schemaIsSpecified = value; }
		}

		/// <summary> Gets or sets if this "hibernate-mapping" attribute is specified </summary>
		public virtual bool HbmDefaultAccessIsSpecified
		{
			get { return _defaultaccessIsSpecified; }
			set { _defaultaccessIsSpecified = value; }
		}

		/// <summary> Gets or sets if this "hibernate-mapping" attribute is specified </summary>
		public virtual bool HbmAssemblyIsSpecified
		{
			get { return _assemblyIsSpecified; }
			set { _assemblyIsSpecified = value; }
		}

		/// <summary> Gets or sets if this "hibernate-mapping" attribute is specified </summary>
		public virtual bool HbmNamespaceIsSpecified
		{
			get { return _namespaceIsSpecified; }
			set { _namespaceIsSpecified = value; }
		}

		/// <summary> Gets or sets if this "hibernate-mapping" attribute is specified </summary>
		public virtual bool HbmDefaultCascadeIsSpecified
		{
			get { return _defaultcascadeIsSpecified; }
			set { _defaultcascadeIsSpecified = value; }
		}
		#endregion


		#region Serialize() for Assemblies
		/// <summary> Build a hbm.xml file for each class in the assembly and write it in the specified directory. </summary>
		/// <param name="directory">Directory where each serialized hbm.xml file will be written.</param>
		/// <param name="assembly">Assembly used to extract user-defined types containing a valid attribute (can be [Class], [Subclass] or [JoinedSubclass]).</param>
		public virtual void Serialize(string directory, System.Reflection.Assembly assembly)
		{
			if(assembly == null)
				throw new System.ArgumentNullException("assembly");

			foreach(System.Type type in assembly.GetTypes())
			{
				if( type.IsNestedFamORAssem || type.IsNestedPrivate || type.IsNestedPublic )
					continue; // will be include in its container mapping file
				if( type.IsDefined( typeof(ClassAttribute), false ) ||
					type.IsDefined( typeof(JoinedSubclassAttribute), false ) ||
					type.IsDefined( typeof(SubclassAttribute), false ) )
					Serialize( System.IO.Path.Combine(directory, type.Name+".hbm.xml"), type );
			}
		}


		/// <summary> Build a hbm.xml file for each class in the assembly and write them in this stream. </summary>
		/// <param name="stream">Where the xml is written.</param>
		/// <param name="assembly">Assembly used to extract user-defined types containing a valid attribute (can be [Class], [Subclass] or [JoinedSubclass]).</param>
		public virtual void Serialize(System.IO.Stream stream, System.Reflection.Assembly assembly)
		{
			if(stream == null)
				throw new System.ArgumentNullException("stream");
			if(assembly == null)
				throw new System.ArgumentNullException("assembly");

			System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter( stream, System.Text.Encoding.UTF8 );
			writer.Formatting = System.Xml.Formatting.Indented;
			writer.WriteStartDocument();
			if(WriteDateComment)
				writer.WriteComment( string.Format( "Generated from NHibernate.Mapping.Attributes on {0}.", System.DateTime.Now.ToString("u") ) );
			WriteHibernateMapping(writer, null);


			// Write classes, subclasses and joined-subclasses (classes must come first if inherited by "external" subclasses)
			foreach(System.Type type in assembly.GetTypes())
			{
				if( type.IsNestedFamORAssem || type.IsNestedPrivate || type.IsNestedPublic )
					continue; // will be include in its container mapping
				if( type.IsDefined( typeof(ClassAttribute), false ) )
					HbmWriter.WriteClass(writer, type);
			}
			foreach(System.Type type in assembly.GetTypes())
			{
				if( type.IsNestedFamORAssem || type.IsNestedPrivate || type.IsNestedPublic )
					continue; // will be include in its container mapping
				if( type.IsDefined( typeof(SubclassAttribute), false ) )
					HbmWriter.WriteSubclass(writer, type);
			}
			foreach(System.Type type in assembly.GetTypes())
			{
				if( type.IsNestedFamORAssem || type.IsNestedPrivate || type.IsNestedPublic )
					continue; // will be include in its container mapping
				if( type.IsDefined( typeof(JoinedSubclassAttribute), false ) )
					HbmWriter.WriteJoinedSubclass(writer, type);
			}

			writer.WriteEndElement(); // </hibernate-mapping>
			writer.WriteEndDocument();
			writer.Flush();

			if( ! Validate )
				return;

			// Validate the generated XML stream
			try
			{
				writer.BaseStream.Position = 0;
				System.Xml.XmlTextReader tr = new System.Xml.XmlTextReader(writer.BaseStream);
				System.Xml.XmlValidatingReader vr = new System.Xml.XmlValidatingReader(tr);

				// Open the Schema
				System.IO.Stream schema = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("NHibernate.Mapping.Attributes.nhibernate-mapping-2.0.xsd");
				vr.Schemas.Add("urn:nhibernate-mapping-2.0", new System.Xml.XmlTextReader(schema));
				vr.ValidationType = System.Xml.ValidationType.Schema;
				vr.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(XmlValidationHandler);

				_stop = false;
				while(vr.Read() && !_stop) // Read to validate (stop at the first error)
					;
			}
			catch(System.Exception ex)
			{
				Error.Append(ex.ToString()).Append(System.Environment.NewLine + System.Environment.NewLine);
			}
		}
		#endregion


		#region Serialize() for Classes
		/// <summary> Build a hbm.xml file for this class and write it in the specified file. </summary>
		/// <param name="filePath">Where the xml is written.</param>
		/// <param name="type">User-defined type containing a valid attribute (can be [Class], [Subclass] or [JoinedSubclass]).</param>
		public virtual void Serialize(string filePath, System.Type type)
		{
			using( System.IO.Stream stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create) )
				Serialize(stream, type);
		}


		/// <summary> Build a hbm.xml file for this class and write it in this stream. </summary>
		/// <param name="stream">Where the xml is written.</param>
		/// <param name="type">User-defined type containing a valid attribute (can be [Class], [Subclass] or [JoinedSubclass]).</param>
		public virtual void Serialize(System.IO.Stream stream, System.Type type)
		{
			Serialize(stream, type, null, true);
		}


		/// <summary> Build a hbm.xml file for this class and write it in this stream. </summary>
		/// <param name="stream">Where the xml is written; can be null if you send the writer.</param>
		/// <param name="type">User-defined type containing a valid attribute (can be [Class], [Subclass] or [JoinedSubclass]).</param>
		/// <param name="writer">The XmlTextWriter used to write the xml; can be null if you send the stream. You can also create it yourself, but don't forget to write the StartElement ("hibernate-mapping") before.</param>
		/// <param name="writeEndDocument">Tells if the EndElement of "hibernate-mapping" must be written; send false to append many classes in the same stream.</param>
		/// <returns>The parameter writer if it was not null; else it is a new writer (created using the stream). Send it back to append many classes in this stream.</returns>
		public virtual System.Xml.XmlTextWriter Serialize(System.IO.Stream stream, System.Type type, System.Xml.XmlTextWriter writer, bool writeEndDocument)
		{
			if(stream == null && writer == null)
				throw new System.ArgumentNullException("stream");
			if(type == null)
				throw new System.ArgumentNullException("type");

			if( writer == null )
			{
				writer = new System.Xml.XmlTextWriter( stream, System.Text.Encoding.UTF8 );
				writer.Formatting = System.Xml.Formatting.Indented;
				writer.WriteStartDocument();
				if(WriteDateComment)
					writer.WriteComment( string.Format( "Generated by NHibernate.Mapping.Attributes on {0}.", System.DateTime.Now.ToString("u") ) );
				WriteHibernateMapping(writer, type);
			}

			if( type.IsDefined( typeof(ClassAttribute), false ) )
				HbmWriter.WriteClass(writer, type);
			else if( type.IsDefined( typeof(SubclassAttribute), false ) )
				HbmWriter.WriteSubclass(writer, type);
			else if( type.IsDefined( typeof(JoinedSubclassAttribute), false ) )
				HbmWriter.WriteJoinedSubclass(writer, type);
			else
				throw new System.ArgumentException("No valid attribute; looking for [Class], [Subclass] or [JoinedSubclass].", "type");

			if(writeEndDocument)
			{
				writer.WriteEndElement(); // </hibernate-mapping>
				writer.WriteEndDocument();
				writer.Flush();

				if( ! Validate )
					return writer;

				// Validate the generated XML stream
				try
				{
					writer.BaseStream.Position = 0;
					System.Xml.XmlTextReader tr = new System.Xml.XmlTextReader(writer.BaseStream);
					System.Xml.XmlValidatingReader vr = new System.Xml.XmlValidatingReader(tr);

					// Open the Schema
					System.IO.Stream schema = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("NHibernate.Mapping.Attributes.nhibernate-mapping-2.0.xsd");
					vr.Schemas.Add("urn:nhibernate-mapping-2.0", new System.Xml.XmlTextReader(schema));
					vr.ValidationType = System.Xml.ValidationType.Schema;
					vr.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(XmlValidationHandler);

					_stop = false;
					while(vr.Read() && !_stop) // Read to validate (stop at the first error)
						;
				}
				catch(System.Exception ex)
				{
					Error.Append(ex.ToString()).Append(System.Environment.NewLine + System.Environment.NewLine);
				}
			}
			else
				writer.Flush();

			return writer;
		}
		#endregion


		#region XmlValidationHandler() and WriteHibernateMapping()
		private void XmlValidationHandler(object sender, System.Xml.Schema.ValidationEventArgs e)
		{
			Error.Append( "Validation error: Severity=" + e.Severity ).Append(System.Environment.NewLine)
				.Append( "Message: " + e.Message )
				.Append(System.Environment.NewLine + System.Environment.NewLine);
			_stop = true;
		}


		private void WriteHibernateMapping(System.Xml.XmlTextWriter writer, System.Type type)
		{
			writer.WriteStartElement( "hibernate-mapping", "urn:nhibernate-mapping-2.0" );

			if(type != null)
			{
				HibernateMappingAttribute attribute = null;
				object[] attributes = type.GetCustomAttributes(typeof(HibernateMappingAttribute), true);
				if(attributes.Length > 0)
				{
					// If the Type has a HibernateMappingAttribute, then we use it
					attribute = attributes[0] as HibernateMappingAttribute;

					// Attribute: <schema>
					if(attribute.Schema != null)
						writer.WriteAttributeString("schema", attribute.Schema);
					// Attribute: <default-cascade>
					if(attribute.DefaultCascade != CascadeStyle.Unspecified)
						writer.WriteAttributeString("default-cascade", HbmWriter.GetXmlEnumValue(typeof(CascadeStyle), attribute.DefaultCascade));
					// Attribute: <default-access>
					if(attribute.DefaultAccess != null)
						writer.WriteAttributeString("default-access", attribute.DefaultAccess);
					// Attribute: <auto-import>
					if(attribute.AutoImportSpecified)
						writer.WriteAttributeString("auto-import", attribute.AutoImport ? "true" : "false");
					// Attribute: <namespace>
					if(attribute.Namespace != null)
						writer.WriteAttributeString("namespace", attribute.Namespace);
					// Attribute: <assembly>
					if(attribute.Assembly != null)
						writer.WriteAttributeString("assembly", attribute.Assembly);
					// Attribute: <default-lazy>
					if(attribute.DefaultLazySpecified)
						writer.WriteAttributeString("default-lazy", attribute.DefaultLazy ? "true" : "false");

					return;
				}
			}

			// Set <hibernate-mapping> attributes using specified properties
			// Attribute: <schema>
			if(_schemaIsSpecified)
				writer.WriteAttributeString("schema", _schema);
			// Attribute: <default-cascade>
			if(_defaultcascadeIsSpecified)
				writer.WriteAttributeString("default-cascade", HbmWriter.GetXmlEnumValue(typeof(CascadeStyle), _defaultcascade));
			// Attribute: <default-access>
			if(_defaultaccessIsSpecified)
				writer.WriteAttributeString("default-access", _defaultaccess);
			// Attribute: <auto-import>
			if(_autoimportIsSpecified)
				writer.WriteAttributeString("auto-import", _autoimport ? "true" : "false");
			// Attribute: <namespace>
			if(_namespaceIsSpecified)
				writer.WriteAttributeString("namespace", _namespace);
			// Attribute: <assembly>
			if(_assemblyIsSpecified)
				writer.WriteAttributeString("assembly", _assembly);
			// Attribute: <default-lazy>
			if(_defaultlazyIsSpecified)
				writer.WriteAttributeString("default-lazy", _defaultlazy ? "true" : "false");
		}
		#endregion
	}
}
