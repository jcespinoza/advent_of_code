pub fn sum_numbers(input: &serde_json::Value, exclusion: Option<&str>) -> i64 {
  match input {
    serde_json::Value::Null => 0,
    serde_json::Value::Bool(_) => 0,
    serde_json::Value::Number(n) => n.as_i64().unwrap(),
    serde_json::Value::String(_) => 0,
    serde_json::Value::Array(arr) => arr.iter().map(|v| sum_numbers(v, exclusion)).sum(),
    serde_json::Value::Object(obj) => {
      if let Some(exclusion) = exclusion {
        if obj.values().any(|v| v == exclusion) {
          return 0;
        }
      }
      obj.values().map(|v| sum_numbers(v, exclusion)).sum()
    }
  }
}
