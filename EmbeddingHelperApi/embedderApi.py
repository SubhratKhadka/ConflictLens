from fastapi import FastAPI
from pydantic import BaseModel
from sentence_transformers import SentenceTransformer

app = FastAPI()

# Load model ONCE (very imp)
model = SentenceTransformer("BAAI/bge-m3")

# @app.get("/")
# def h ():
#     return {"message": "it works"}

# single embed req
class EmbedRequest(BaseModel):
    text: str

@app.post("/embed")
def embed(req: EmbedRequest):
    if not req.text.strip():
        return {"error": "Empty text"}
    
    embedding = model.encode(
        req.text,
        normalize_embeddings=True
    )
    
    return {
        "embedding": embedding.tolist(),
        "dimension": len(embedding)
    }

# for batch request for embedding
class BatchRequest(BaseModel):
    texts: list[str]

@app.post("/embed-batch")
def embed_batch(req: BatchRequest):
    if not req.texts or not all(t.strip() for t in req.texts):
        return {"error": "Empty text in list"}
    
    embeddings = model.encode(
        req.texts,
        normalize_embeddings=True
    )
    
    return {
        "embeddings": [e.tolist() for e in embeddings]
    }