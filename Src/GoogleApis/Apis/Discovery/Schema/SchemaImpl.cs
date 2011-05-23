// /*
// Copyright 2010 Google Inc
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// */
using System;

using System.Collections.Generic;
using System.IO;
using System.Text;

using Newtonsoft.Json.Schema;

using Google.Apis.Json;
using Google.Apis.Requests;
using Google.Apis.Testing;
using Google.Apis.Util;

namespace Google.Apis.Discovery.Schema
{
    internal class SchemaImpl : ISchema
    {
        private static readonly log4net.ILog logger = log4net.LogManager.GetLogger (typeof(SchemaImpl));
        
        private readonly string name;
        private readonly JsonSchema schema;
        
        public SchemaImpl (string name, string jsonSchemaDefinition, FutureJsonSchemaResolver resolver)
        {
            name.ThrowIfNullOrEmpty("name");
            jsonSchemaDefinition.ThrowIfNullOrEmpty("jsonSchemaDefinition");
            resolver.ThrowIfNull("resolver");
            
            this.name = name;
            logger.Debug("Parsing Schema " + this.name);
            try{
                this.schema = JsonSchema.Parse(jsonSchemaDefinition, resolver);
            } catch (Exception ex) 
            {
                throw new ApplicationException(
                    string.Format("Failed to parse schema [{0}] which was defined as [{1}]", name, jsonSchemaDefinition),
                    ex);
            }
        }
        
        public string Name { get { return name;} }
        public string Id { get { return schema.Id;}}
        public JsonSchema SchemaDetails { get { return schema;}}
    }
}
