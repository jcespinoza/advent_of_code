This Rust project houses the Advent of Code solutions in the following directory structure:

`src > days > dayxy`

Because of Rust semantics and lack of a better solution, the code for a new daily challenge is a duplicate of the structs in the directory `templates` which contain boilerplate for a Solver struct which allows for independent testing of the parsing and the actual solving logic.

To create a new daily challenge, use the script `create_day.sh` like so:

```bash
./create_day.sh --day 1
```

This script creates valid rust code for the given day with `!unimplemented()` as the body for functions and unit tests are ignored by default.

The `dayxx_lib.rs` is meant to hold helper code to keep the solver light. The `dayxx_impl.rs` is not meant to be modified; it merely used to provide a consistent way to invoke the solver functions without having to know the data types that it uses internally.

The unit tests in `dayxx_tests.rs` allows for the testing of the sample data usually provided in each challenge. Separate unit tests can be created for other scenarios, these are prefixed with "examples_" to distinguish them and handle an array of example input and expected output.