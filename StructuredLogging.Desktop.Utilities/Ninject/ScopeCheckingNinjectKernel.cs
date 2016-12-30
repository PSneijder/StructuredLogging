﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ninject;
using Ninject.Activation;
using Ninject.Planning.Bindings;

namespace StructuredLogging.Desktop.Utilities.Ninject
{
    /// <summary>
    /// A <see cref="StandardKernel" /> which additionally checks that injected objects are correctly scoped.
    /// </summary>
    /// <seealso cref="StandardKernel" />
    public sealed class ScopeCheckingStandardKernel
        : StandardKernel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScopeCheckingStandardKernel" /> class.
        /// </summary>
        public ScopeCheckingStandardKernel()
        {
            AllowPerRequestScopeInTransientScopedController = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether a transient scoped object is allowed in a singleton scoped object.
        /// </summary>
        /// <value>
        /// <c>true</c> if [allow transient scope in singleton scope]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowTransientScopeInSingletonScope { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a transient scoped object is allowed in a thread scoped object.
        /// </summary>
        /// <value>
        /// <c>true</c> if [allow transient scope in thread scope]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowTransientScopeInThreadScope { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a transient scoped object is allowed in a custom scoped object.
        /// </summary>
        /// <value>
        /// <c>true</c> if [allow transient scope in custom scope]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowTransientScopeInCustomScope { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a per-request scoped object is allowed in a transient scoped controller object (default: true).
        /// </summary>
        /// <value>
        /// <c>true</c> if [allow per request scope in transient scoped controller]; otherwise, <c>false</c>.
        /// </value>
        public bool AllowPerRequestScopeInTransientScopedController { get; set; }

        /// <summary>
        /// Resolves instances for the specified request.
        /// The instances are not actually resolved until a consumer iterates over the enumerator.
        /// </summary>
        /// <param name="request">The request to resolve.</param>
        /// <returns>
        /// An enumerator of instances that match the request.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">The scope of the injected object ( + request.Service.FullName + )  +
        ///                                                         is not compatible with the scope of the parent object ( + parentBinding.Service.FullName + ).</exception>
        /// <exception cref="InvalidOperationException">The scope of the injected object is not compatible with the scope of the parent object.</exception>
        public override IEnumerable<object> Resolve(IRequest request)
        {
            var isInjectedIntoParent = request.ActiveBindings.Any();
            if (isInjectedIntoParent)
            {
                var parentBinding = request.ActiveBindings.Last();

                var bindings = GetBindings(request.Service).Where(SatifiesRequest(request));
                if (bindings.Any(binding => IsScopeAllowed(request, binding, parentBinding) == false))
                {
                    throw new InvalidOperationException("The scope of the injected object (" + request.Service.FullName + ") " +
                                                        "is not compatible with the scope of the parent object (" + parentBinding.Service.FullName + ").");
                }
            }

            return base.Resolve(request);
        }

        /// <summary>
        /// Determines whether [is scope allowed] by [the specified request].
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="binding">The binding.</param>
        /// <param name="parentBinding">The parent binding.</param>
        /// <returns></returns>
        private bool IsScopeAllowed(IRequest request, IBinding binding, IBinding parentBinding)
        {
            var scope = binding.GetScope(CreateContext(request, binding));
            var parentScope = parentBinding.GetScope(CreateContext(request, parentBinding));

            var haveSameScope = scope == parentScope;
            if (haveSameScope)
                return true;

            var isChildSingletonScoped = scope == this;
            if (isChildSingletonScoped)
                return true;

            var isChildTransientScoped = scope == null;
            var isChildPerRequestScoped = scope != null && scope.GetType().Name == "HttpContext";

            var isParentSingletonScoped = parentScope == this;
            if (isParentSingletonScoped)
                return AllowTransientScopeInSingletonScope && isChildTransientScoped;

            var isParentThreadScoped = parentScope is Thread;
            if (isParentThreadScoped)
                return AllowTransientScopeInThreadScope && isChildTransientScoped;

            var isParentAController = parentBinding.Service.Name.EndsWith("Controller");
            var isParentTransientScoped = parentScope == null;
            if (isParentTransientScoped)
                return AllowPerRequestScopeInTransientScopedController && isParentAController && isChildPerRequestScoped;

            return AllowTransientScopeInCustomScope && isChildTransientScoped;
        }
    }
}