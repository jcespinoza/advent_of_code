extern crate proc_macro;
use proc_macro::TokenStream;
use quote::quote;
use syn::{parse_macro_input, DeriveInput};

// #[proc_macro_derive(HasDayYear)]
pub fn derive_has_day_year(input: TokenStream) -> TokenStream {
  let input = parse_macro_input!(input as DeriveInput);
  let name = input.ident;

  let expanded = quote! {
      impl HasDayYear for #name {
          fn day(&self) -> i32 {
              self.day
          }

          fn year(&self) -> i32 {
              self.year
          }
      }
  };

  TokenStream::from(expanded)
}
