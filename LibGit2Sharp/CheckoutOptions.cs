﻿using LibGit2Sharp.Core;
using LibGit2Sharp.Handlers;

namespace LibGit2Sharp
{
    /// <summary>
    /// Collection of parameters controlling Checkout behavior.
    /// </summary>
    public sealed class CheckoutOptions : IConvertableToGitCheckoutOpts
    {
        /// <summary>
        /// Options controlling checkout behavior.
        /// </summary>
        public CheckoutModifiers CheckoutModifiers { get; set; }

        /// <summary>
        /// Delegate that checkout progress will be reported through.
        /// </summary>
        public CheckoutProgressHandler OnCheckoutProgress { get; set; }

        /// <summary>
        /// Delegate to be called during checkout for files that match
        /// desired filter specified with the NotifyFlags property.
        /// </summary>
        public CheckoutNotifyHandler OnCheckoutNotify { get; set; }

        /// <summary>
        /// The Flags specifying what notifications are reported.
        /// </summary>
        public CheckoutNotifyFlags CheckoutNotifyFlags { get; set; }

        CheckoutStrategy IConvertableToGitCheckoutOpts.CheckoutStrategy
        {
            get
            {
                return CheckoutModifiers.HasFlag(CheckoutModifiers.Force) ?
                    CheckoutStrategy.GIT_CHECKOUT_FORCE : CheckoutStrategy.GIT_CHECKOUT_SAFE;
            }
        }

        /// <summary>
        /// Generate a <see cref="CheckoutCallbacks"/> object with the delegates
        /// hooked up to the native callbacks.
        /// </summary>
        /// <returns></returns>
        CheckoutCallbacks IConvertableToGitCheckoutOpts.GenerateCallbacks()
        {
            return CheckoutCallbacks.From(OnCheckoutProgress, OnCheckoutNotify);
        }
    }
}
