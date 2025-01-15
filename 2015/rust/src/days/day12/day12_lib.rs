pub fn sum_numbers(input: &serde_json::Value) -> i64 {
  match input {
    serde_json::Value::Null => 0,
    serde_json::Value::Bool(_) => 0,
    serde_json::Value::Number(n) => n.as_i64().unwrap(),
    serde_json::Value::String(_) => 0,
    serde_json::Value::Array(arr) => arr.iter().map(|v| sum_numbers(v)).sum(),
    serde_json::Value::Object(obj) => obj.values().map(|v| sum_numbers(v)).sum(),
  }
}
