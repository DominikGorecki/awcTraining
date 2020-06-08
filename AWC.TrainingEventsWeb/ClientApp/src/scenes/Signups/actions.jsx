import { activitiesApi } from '../../services/api'
import {SuccessResponse,FailResponse} from '../../services/CallResponses'

export const GetSignups = async () => {
    try {
        var result = await activitiesApi().get();
        return SuccessResponse(result.data);
    }
    catch (e)
    {
        return FailResponse(e.response.data);
    }
}

