#region License Header
// /*******************************************************************************
//  * Open Behavioral Health Information Technology Architecture (OBHITA.org)
//  * 
//  * Redistribution and use in source and binary forms, with or without
//  * modification, are permitted provided that the following conditions are met:
//  *     * Redistributions of source code must retain the above copyright
//  *       notice, this list of conditions and the following disclaimer.
//  *     * Redistributions in binary form must reproduce the above copyright
//  *       notice, this list of conditions and the following disclaimer in the
//  *       documentation and/or other materials provided with the distribution.
//  *     * Neither the name of the <organization> nor the
//  *       names of its contributors may be used to endorse or promote products
//  *       derived from this software without specific prior written permission.
//  * 
//  * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//  * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//  * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//  * DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
//  * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//  * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//  * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//  * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//  * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//  * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//  ******************************************************************************/
#endregion
namespace ProCenter.Domain.AssessmentModule
{
    #region Using Statements

    using System;
    using System.Collections.Generic;
    using CommonModule.Lookups;
    using Event;

    #endregion

    public class ScoreItem : IEquatable<ScoreItem>
    {
        public bool Equals ( ScoreItem other )
        {
            if ( ReferenceEquals ( null, other ) )
                return false;
            if ( ReferenceEquals ( this, other ) )
                return true;
            return string.Equals ( ItemDefinitionCode, other.ItemDefinitionCode ) && Equals ( Value, other.Value );
        }

        public override bool Equals ( object obj )
        {
            if ( ReferenceEquals ( null, obj ) )
                return false;
            if ( ReferenceEquals ( this, obj ) )
                return true;
            if ( obj.GetType () != this.GetType () )
                return false;
            return Equals ( (ScoreItem) obj );
        }

        public override int GetHashCode ()
        {
            unchecked
            {
                return ( ( ItemDefinitionCode != null ? ItemDefinitionCode.GetHashCode () : 0 ) * 397 ) ^ ( Value != null ? Value.GetHashCode () : 0 );
            }
        }

        public static bool operator == ( ScoreItem left, ScoreItem right )
        {
            return Equals ( left, right );
        }

        public static bool operator != ( ScoreItem left, ScoreItem right )
        {
            return !Equals ( left, right );
        }

        public ScoreItem(string itemDefinitionCode, object value, params ScoreItem[] scoreItems)
        {
            ItemDefinitionCode = itemDefinitionCode;
            Value = value;
            ScoreItems = scoreItems;
        }

        public string ItemDefinitionCode { get; private set; }

        public Lookup ValueType { get; private set; } //todo: need this or list of SocreItems?

        public object Value { get; private set; }

        public ItemMetadata ItemMetadata { get; set; }

        public IEnumerable<ScoreItem> ScoreItems { get; private set; }

        public void UpdateValue ( object value )
        {
            Value = value;
        }
    }
}