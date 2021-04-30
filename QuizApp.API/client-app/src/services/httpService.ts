import ky, { Options } from "ky";
import logger from "./logService";
import { toast } from "react-toastify";
import apiEndpoint from "../config.json"

const {apiUrl: baseUrl} = apiEndpoint;

let token : string | null = null;

const errorHandling = (_request : Request, _options : Options, response : Response) => {
  if (!(response.status >= 400 && response.status < 500)) {
    logger.log(response);
    toast.error("An unexpected error occurrred.");
  }

  return Promise.reject(response);
};

const addJwtToHeader = (request : Request, options : Options) =>{
  request.headers.set('x-auth-token', `${token}`);

	return ky(request);
}

function setJwt(jwt : string | null) {
  token = jwt;
}

const kyInstance = ky
  .create({prefixUrl: baseUrl})
  .extend({
    hooks: {
      afterResponse: [errorHandling],
      beforeRequest: [addJwtToHeader]
    },
  });

const http = {
  get: kyInstance.get, 
  post: kyInstance.post, 
  put: kyInstance.put, 
  delete: kyInstance.delete, 
  setJwt
};

export default http;
