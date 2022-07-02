import requests
import json

response_API = requests.get('https://localhost:44345/api/v1/Food/GetByCode/FC-001', verify=False)
print(json.loads(response_API.text))