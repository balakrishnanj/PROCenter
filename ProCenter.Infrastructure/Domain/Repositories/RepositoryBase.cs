﻿#region License Header

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

namespace ProCenter.Infrastructure.Domain.Repositories
{
    #region Using Statements

    using System;

    using Pillar.Common.InversionOfControl;

    using ProCenter.Domain.CommonModule;
    using ProCenter.Infrastructure.EventStore;

    #endregion

    /// <summary>Repository Base.</summary>
    /// <typeparam name="TAggregate">The type of the aggregate.</typeparam>
    public abstract class RepositoryBase<TAggregate> : IRepository<TAggregate>
        where TAggregate : class, IAggregateRoot
    {
        #region Fields

        private readonly IUnitOfWorkProvider _unitOfWorkProvider;

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="RepositoryBase{TAggregate}" /> class.</summary>
        /// <param name="unitOfWorkProvider">The unit of work provider.</param>
        protected RepositoryBase ( IUnitOfWorkProvider unitOfWorkProvider )
        {
            _unitOfWorkProvider = unitOfWorkProvider;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>Gets the event store repository.</summary>
        /// <value>The event store repository.</value>
        protected IEventStoreRepository EventStoreRepository
        {
            get
            {
                var unitOfWork = _unitOfWorkProvider.GetCurrentUnitOfWork ();
                return unitOfWork.EventStoreRepository;
            }
        }

        /// <summary>
        ///     Gets the aggregate by key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The aggregate.</returns>
        public TAggregate GetByKey ( Guid key )
        {
            return EventStoreRepository.GetByKey<TAggregate> ( key );
        }

        /// <summary>
        ///     Gets the last modified date.
        /// </summary>
        /// <param name="key">The aggregate key.</param>
        /// <returns>Last modified date.</returns>
        public DateTime? GetLastModifiedDate ( Guid key )
        {
            return EventStoreRepository.GetLastModifiedDate<TAggregate> ( key );
        }

        #endregion
    }
}